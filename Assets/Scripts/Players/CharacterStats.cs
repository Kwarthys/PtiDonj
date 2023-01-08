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

    public void takeDamage(float amount)
    {
        life -= amount;
    }

    private void updateLifeDisplayHook(float oldLife, float newLife)
    {
        life = newLife;
        displayManager.updateLifeDisplay(life / maxLife, life);
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

    private void Start()
    {
        //forcing an update
        updateLifeDisplayHook(0, maxLife);
    }

    public void updateStats()
    {
        updateEffects();

        //Update UI
        displayManager.updateDisplayOrientation();
    }

    private void updateEffects()
    {
        Effect.updateEffects(effects);
    }
}
