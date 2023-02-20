using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class CharacterStats : NetworkBehaviour
{
    [SerializeField]
    [SyncVar(hook = nameof(updateLifeDisplayHook))]
    private float life = 0.1f;

    [SerializeField]
    private float maxLife = 100;

    [SerializeField]
    private List<Effect> effects = new List<Effect>();

    public StatsDisplayManager displayManager;

    public NetworkIdentity networkIdentity;

    public AbilityManager abilityManager;

    public Transform followingZoneHolder;
    public Transform foot;

    private MovementController controller; //will have to change this to account for monster movement aswell
    private PlayerInputHandler playerInputs; //will stay null on a mosnter

    private MovementEffector currentEffector;

    public bool moving { get; private set; } = false;

    private void Awake()
    {
        life = maxLife;
    }

    private void Start()
    {
        //forcing an update
        updateLifeDisplayHook(-1, life);

        controller = GetComponent<MovementController>();
        playerInputs = GetComponent<PlayerInputHandler>();
    }

    public void notifyPlayerMovementChange(bool isMoving)
    {
        moving = isMoving;
        CmdNotifyPlayerMovementChange(isMoving);
    }

    [Command(requiresAuthority = false)]
    public void CmdNotifyPlayerMovementChange(bool isMoving)
    {
        moving = isMoving;
    }

    public float getCurrentLife()
    {
        return life;
    }

    public float getCurrentLifeRelative()
    {
        return life / maxLife;
    }

    public void takeDamage(float amount, NetworkIdentity attacker, NetworkIdentity damaged)
    {
        life -= amount;

        GameManager.instance.spawnFloatingTextFor(transform.position, Mathf.RoundToInt(amount).ToString(), attacker, damaged);
    }

    private void updateLifeDisplayHook(float oldLife, float newLife)
    {
        life = newLife;
        displayManager.changeLifeDisplay(life / maxLife, life, oldLife==-1);
    }

    public void removeEffect(Effect effect)
    {
        effects.Remove(effect);
    }

    public void addEffect(Effect effect)
    {
        effect.owner = this;

        effect.onStart();

        if(effect.effectOnDuration)
        {
            effects.Add(effect);
        }
    }

    public void setupCastBarAnimation(float animationDuration, string text, CastBarDisplayController.FillMode fillMode)
    {
        displayManager.setCastBar(animationDuration, text, fillMode);
    }

    public void interruptCastBarAnimation()
    {
        displayManager.interruptCastBar();
    }

    public void registerNewDisplayManager(StatsDisplayManager newManager)
    {
        displayManager = newManager;
        updateLifeDisplayHook(-1, life);
    }

    public void updateStats()
    {
        updateEffects();
        updateAbilities(false); //called from server, so we disable the anti server update of "clientOnly"
    }

    public void updateAbilities(bool clientOnly = true)
    {
        if(!isServer || !clientOnly)
        {
            updateMovement(); //ugly debug call right here, will refactor later

            if (abilityManager != null) //will only be true on players, not monsters (yet ?)
            {
                abilityManager.updateAbilities(clientOnly);
            }
        }
    }

    public void registerNewMovementEffector(MovementEffector effector, AbilityTargetingData targeting)
    {
        currentEffector = effector;
        effector.startMovement(targeting);
    }

    public void updateMovement()
    {
        if(playerInputs != null)
        {
            bool moveLock = false;

            if(currentEffector != null)
            {
                moveLock = currentEffector.locksInputs;
            }

            if(!moveLock)
            {
                controller.updateMovements(playerInputs.getInputs());
            }
        }

        if(currentEffector != null)
        {
            bool keepEffector = currentEffector.updateMovement();

            if(currentEffector.outputsMoveCommands)
            {
                controller.updateMovements(currentEffector.getMoveCommands());
            }

            if(!keepEffector)
            {
                currentEffector = null;
            }
        }
    }

    public Vector3 getFootBodyOffset()
    {
        return transform.position - foot.position;
    }

    public void updateDisplay()
    {
        //Update UI
        displayManager.updateDisplay();
    }

    private void updateEffects()
    {
        Effect.updateEffects(effects);
    }
}
