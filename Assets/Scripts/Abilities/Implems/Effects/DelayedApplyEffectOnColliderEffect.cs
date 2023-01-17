using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedApplyEffectOnColliderEffect : OnTimeEffect, IColliderEffect
{
    public LayerMask targetsLayer;

    public EffectDescriptor[] effectsToApply;

    public ColliderTriggerHandler colliderTriggers;

    public float size;

    public float getZoneSize()
    {
        return size;
    }

    public override void onEnd()
    {
        List<CharacterStats> characters = colliderTriggers.getTargetsInside(targetsLayer);

        for (int i = 0; i < characters.Count; i++)
        {
            applyEffectsTo(characters[i], effectsToApply);
        }
    }

    public void registerColliderTrigger(ColliderTriggerHandler triggerHandler)
    {
        this.colliderTriggers = triggerHandler;
    }
}
