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

        if(effect.needsWorldPosUpdate)
        {
            if(AbilityTargetingData.tryFindGroundUnder(effect.owner.transform.position, out Vector3 ground))
            {
                effect.effectWorldPos = ground;
            }
        }

        effect.onStart();

        if(effect.effectOnDuration)
        {
            effects.Add(effect);
        }
    }

    private void Awake()
    {
        life = maxLife;
    }

    private void Start()
    {
        //forcing an update
        updateLifeDisplayHook(-1, life);
    }

    public void registerNewDisplayManager(StatsDisplayManager newManager)
    {
        displayManager = newManager;
        updateLifeDisplayHook(-1, life);
    }

    public void updateStats()
    {
        updateEffects();

        //Update UI
        displayManager.updateDisplay();
    }

    private void updateEffects()
    {
        Effect.updateEffects(effects);
    }
}
