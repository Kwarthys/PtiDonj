using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedApplyEffectOnColliderEffectDescriptor : EffectDescriptor
{
    public LayerMask targetsLayer;

    [HideInInspector]
    public ColliderTriggerHandler colliderTriggers;

    public EffectDescriptor[] effectsToApplyOnCharacters;

    public float effectDuration;

    public float size;

    public override Effect getNewEffect()
    {
        DelayedApplyEffectOnColliderEffect effect = new DelayedApplyEffectOnColliderEffect(effectName, true, effectDuration);

        effect.targetsLayer = targetsLayer;
        effect.effectsToApplyOnCharacters = effectsToApplyOnCharacters;
        effect.colliderTriggers = colliderTriggers;

        effect.size = size;

        effect.associatedGameObject = gameObject;

        return effect;
    }
}
