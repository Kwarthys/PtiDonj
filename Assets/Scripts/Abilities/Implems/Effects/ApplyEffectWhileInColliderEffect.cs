using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyEffectWhileInColliderEffect : TickingEffect, IColliderEffect
{
    public Collider areaOfEffect;

    public EffectDescriptor[] effectsToApply;

    public LayerMask targetsLayer;

    public ColliderTriggerHandler colliderTriggers;

    public float size;

    public bool includeOwnerIfAny = false;

    public ApplyEffectWhileInColliderEffect(string effectName, float effectDuration, float effectTickCooldown) : base(effectName, effectDuration, effectTickCooldown)
    {

    }

    public float getZoneSize()
    {
        return size;
    }

    public override void onTick()
    {
        List<CharacterStats> targetsInside = colliderTriggers.getTargetsInside(targetsLayer);

        for (int j = 0; j < targetsInside.Count; j++)
        {
            applyEffectsTo(targetsInside[j], effectsToApply);
        }

        if(includeOwnerIfAny)
        {
            if(owner != null)
            {
                applyEffectsTo(owner, effectsToApply);
            }
        }
    }

    public void registerColliderTrigger(ColliderTriggerHandler triggerHandler)
    {
        this.colliderTriggers = triggerHandler;
    }
}
