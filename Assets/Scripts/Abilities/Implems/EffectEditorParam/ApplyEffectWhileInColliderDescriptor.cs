using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyEffectWhileInColliderDescriptor : EffectDescriptor
{
    public EffectDescriptor[] effectsToApply;

    public LayerMask targetsLayer;

    public float effectDuration;

    public float effectTickCooldown;

    public ColliderTriggerHandler colliderTriggers;

    public override Effect getNewEffect()
    {
        ApplyEffectWhileInColliderEffect effect = new ApplyEffectWhileInColliderEffect();

        effect.effectDuration = this.effectDuration;
        effect.effectOnDuration = true;
        effect.effectTickCooldown = this.effectTickCooldown;
        effect.targetsLayer = this.targetsLayer;
        
        effect.colliderTriggers = this.colliderTriggers;

        effect.effectsToApply = this.effectsToApply;

        effect.associatedGameObject = gameObject;

        return effect;
    }
}