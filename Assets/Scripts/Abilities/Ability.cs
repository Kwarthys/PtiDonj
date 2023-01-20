using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public abstract class Ability : MonoBehaviour
{
    public float cooldown = 1;
    private float lastCast = -1;
    public enum AbilityType { Basic, Combat, Movement, Finisher}

    public AbilityType type;

    public LayerMask targetLayer;

    public bool selfOnlyIfTarget = false;

    public EffectDescriptor[] targetEffects;
    public EffectDescriptor[] selfEffects;
    public EffectDescriptor[] groundEffects;

    protected AbilityManager manager;
    protected CharacterStats ownerStats;

    private void Start()
    {
        manager = GetComponentInParent<AbilityManager>();
        ownerStats = GetComponentInParent<CharacterStats>();

        lastCast = -cooldown;
    }

    public bool canCast()
    {
        return Time.realtimeSinceStartup - lastCast > cooldown;
    }

    public bool tryCastAbility()
    {
        bool casted = false;

        if (canCast())
        {
            AbilityTargetingData targeting = computeTargeting();

            casted = true;
            lastCast = Time.realtimeSinceStartup;

            applyAbility(targeting);

            //for additional effect such as dash or complex logic
            onCast();
        }

        return casted;
    }

    protected abstract AbilityTargetingData computeTargeting();

    private void applyAbility(AbilityTargetingData targeting)
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
            Effect effect = targetEffects[i].getNewEffect();
            effect.caster = ownerStats;
            target.addEffect(effect);
        }
    }

    private void applyAbilitySelf()
    {
        for (int i = 0; i < selfEffects.Length; ++i)
        {
            Effect effect = selfEffects[i].getNewEffect();
            effect.caster = ownerStats;
            manager.selfStats.addEffect(effect);
        }
    }

    private void applyAbilityGround(Vector3 posOnGround)
    {
        for (int i = 0; i < groundEffects.Length; ++i)
        {
            Effect effect = groundEffects[i].getNewEffect();
            effect.effectWorldPos = posOnGround;
            effect.caster = ownerStats;
            GameManager.instance.addGroundEffect(effect);
        }
    }
    protected virtual void onCast() { }
    public void drawDebugCrossAtPoint(Vector3 worldPoint)
    {
        Debug.DrawLine(worldPoint + Vector3.left, worldPoint + Vector3.right * 2, Color.red, 5);
        Debug.DrawLine(worldPoint + Vector3.forward, worldPoint + Vector3.back * 2, Color.blue, 5);
        Debug.DrawLine(worldPoint + Vector3.up, worldPoint + Vector3.down * 2, Color.yellow, 5);
    }
}
