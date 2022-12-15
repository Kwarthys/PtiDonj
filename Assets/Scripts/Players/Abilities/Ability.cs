using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public float cooldown = 1;
    private float lastCast = -1;
    public enum AbilityType { Basic, Combat, Movement, Finisher}

    public AbilityType type;

    public LayerMask targetLayer;

    public bool selfOnlyIfTarget = false;

    public Effect[] targetEffects;
    public Effect[] selfEffects;
    public Effect[] groundEffects;

    private AbilityManager manager;

    private void Start()
    {
        manager = GetComponentInParent<AbilityManager>();
    }

    public bool tryCastAbility()
    {
        bool casted = false;

        if(Time.realtimeSinceStartup - lastCast > cooldown)
        {
            casted = true;
            lastCast = Time.realtimeSinceStartup;

            AbilityTargetingResult targeting = manager.tryGetTarget(targetLayer);
            applyAbility(targeting);

            //for additional effect such as dash or complex logic
            onCast();
        }

        return casted;
    }

    private void applyAbility(AbilityTargetingResult targeting)
    {
        if (!selfOnlyIfTarget || targeting.charHit != null) //only apply if we don't need a target to do so, or if we do have a target
        {
            applyAbilitySelf();
        }

        if (targeting.charHit != null)
        {
            applyAbilityTarget(targeting.charHit);
        }

        applyAbilityGround(targeting.pointHit);
    }

    private void applyAbilityTarget(CharacterStats target)
    {
        for (int i = 0; i < targetEffects.Length; ++i)
        {
            target.addEffect(targetEffects[i]);
        }
    }

    private void applyAbilitySelf()
    {
        for (int i = 0; i < selfEffects.Length; ++i)
        {
            manager.selfStats.addEffect(selfEffects[i]);
        }
    }

    private void applyAbilityGround(Vector3 posOnGround)
    {

        /*
        for (int i = 0; i < groundEffects.Length; ++i)
        {
            //add groundEffects[i] to ground
            // not sure yet on how to do that, spawner effect called here ?
        }
        */
    }


    protected virtual void onCast() { }
}
