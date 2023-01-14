using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedApplyEffectOnColliderEffectDescriptor : EffectDescriptor
{
    public LayerMask targetsLayer;

    public EffectDescriptor[] effectsToApply;

    public ColliderTriggerHandler colliderTriggers;

    public float effectDuration;

    public override Effect getNewEffect()
    {
        DelayedApplyEffectOnColliderEffect effect = new DelayedApplyEffectOnColliderEffect();

        effect.targetsLayer = targetsLayer;
        effect.effectsToApply = effectsToApply;
        effect.colliderTriggers = colliderTriggers;

        effect.effectOnDuration = true;
        effect.effectDuration = effectDuration;

        effect.associatedGameObject = gameObject;

        return effect;
    }
}
