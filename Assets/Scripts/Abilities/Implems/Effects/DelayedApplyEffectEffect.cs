using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedApplyEffectEffect : OnTimeEffect
{
    public List<EffectDescriptor> effectsToApply;

    public DelayedApplyEffectEffect(string effectName, float effectDuration, List<EffectDescriptor> effectsToApply) : base(effectName, effectDuration)
    {
        this.effectsToApply = effectsToApply;
    }

    public override void onEnd()
    {
        applyEffectsTo(owner, effectsToApply.ToArray());
    }
}
