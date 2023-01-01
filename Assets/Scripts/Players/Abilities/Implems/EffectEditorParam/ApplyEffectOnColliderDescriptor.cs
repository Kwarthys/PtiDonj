using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyEffectOnColliderDescriptor : EffectDescriptor
{
    public EffectDescriptor[] effectsToApply;

    public LayerMask targetsLayer;

    public float effectDuration;

    public float effectTickCooldown;

    public EffectColliderTrigger colliderTriggers;

    public override Effect getNewEffect()
    {
        ApplyEffectOnColliderEffect effect = new ApplyEffectOnColliderEffect();

        effect.effectDuration = this.effectDuration;
        effect.effectOnDuration = true;
        effect.effectTickCooldown = this.effectTickCooldown;
        effect.targetsLayer = this.targetsLayer;
        
        effect.registerColliderTriggers(this.colliderTriggers);

        effect.effectsToApply = this.effectsToApply;

        return effect;
    }
}
