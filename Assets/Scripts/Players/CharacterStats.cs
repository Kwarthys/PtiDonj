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

    public Transform followingZoneHolder { get; private set; }

    public bool moving = false;

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

    private void Awake()
    {
        life = maxLife;

        GameObject holderGO = new GameObject("FollowingZoneHolder");
        followingZoneHolder = holderGO.transform;
        followingZoneHolder.parent = transform;
    }

    private void Start()
    {
        //forcing an update
        updateLifeDisplayHook(-1, life);
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

        if(abilityManager != null) //will only be true on players, not monsters (yet ?)
        {
            abilityManager.updateAbilities();
        }
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
