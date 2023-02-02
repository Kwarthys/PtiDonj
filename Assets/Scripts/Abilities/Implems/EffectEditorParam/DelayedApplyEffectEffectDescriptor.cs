using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedApplyEffectEffectDescriptor : EffectDescriptor
{
    public List<EffectDescriptor> effectsToApplyOnEnd;
    public float applyDelay = 1;

    public override Effect getNewEffect()
    {
        return new DelayedApplyEffectEffect(effectName, applyDelay, effectsToApplyOnEnd);
    }
}
