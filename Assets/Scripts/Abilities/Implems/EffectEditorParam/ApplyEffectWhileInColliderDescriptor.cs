using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyEffectWhileInColliderDescriptor : EffectDescriptor
{
    public EffectDescriptor[] effectsToApply;

    public LayerMask targetsLayer;

    public float effectDuration;

    public float effectTickCooldown;

    [HideInInspector]
    public ColliderTriggerHandler colliderTriggers;

    public float zoneSize;

    public override Effect getNewEffect()
    {
        ApplyEffectWhileInColliderEffect effect = new ApplyEffectWhileInColliderEffect(effectName, effectDuration, effectTickCooldown);

        effect.targetsLayer = this.targetsLayer;
        effect.size = zoneSize;        
        effect.colliderTriggers = this.colliderTriggers;
        effect.effectsToApply = this.effectsToApply;
        effect.associatedGameObject = gameObject;

        return effect;
    }
}
