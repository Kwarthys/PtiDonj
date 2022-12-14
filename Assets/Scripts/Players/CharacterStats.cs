using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField]
    private float life = 100;

    [SerializeField]
    private List<Effect> effects = new List<Effect>();

    public void takeDamage(float amount)
    {
        life -= amount;

        Debug.Log("Took " + amount + " damage.");
    }

    public void addEffect(Effect effect)
    {
        effect.owner = this;
        effect.onStart();

        if(effect.effectDuration > 0)
        {
            effects.Add(effect);
        }
    }

    public void updateEffects()
    {
        for (int i = 0; i < effects.Count; i++)
        {

        }
    }
}
