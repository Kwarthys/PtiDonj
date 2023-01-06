using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class CharacterStats : NetworkBehaviour
{
    [SerializeField]
    [SyncVar(hook = nameof(updateLifeDisplayHook))]
    private float life = 100;

    [SerializeField]
    private List<Effect> effects = new List<Effect>();

    public TextMeshPro lifeText;

    public void takeDamage(float amount)
    {
        life -= amount;
    }

    private void updateLifeDisplayHook(float oldLife, float newLife)
    {
        life = newLife;
        lifeText.text = life.ToString();
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
        lifeText.text = life.ToString();
    }

    public void updateStats()
    {
        updateEffects();

        //Update UI
        lifeText.transform.rotation = Quaternion.LookRotation(lifeText.transform.position - GameManager.instance.localPlayerTransform.position);
    }

    private void updateEffects()
    {
        Effect.updateEffects(effects);
    }
}
