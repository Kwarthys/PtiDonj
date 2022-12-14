using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public float cooldown = 1;
    private float lastCast = -1;

    public enum AbilityType { Basic, Combat, Movement, Finisher}

    public AbilityType type;

    private Effect[] abilityEffects;

    private void Start()
    {
        abilityEffects = GetComponents<Effect>();
    }

    public bool tryCastAbility()
    {
        bool casted = false;

        if(Time.realtimeSinceStartup - lastCast > cooldown)
        {
            casted = true;
            lastCast = Time.realtimeSinceStartup;
            onCast(abilityEffects);
        }

        return casted;
    }


    protected abstract void onCast(Effect[] abilityEffects);
}
