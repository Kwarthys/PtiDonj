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

    public ApplyEffectWhileInColliderEffect(string effectName, bool effectOnDuration, float effectDuration, float effectTickCooldown) : base(effectName, effectOnDuration, effectDuration, effectTickCooldown)
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
    }

    public void registerColliderTrigger(ColliderTriggerHandler triggerHandler)
    {
        this.colliderTriggers = triggerHandler;
    }
}
