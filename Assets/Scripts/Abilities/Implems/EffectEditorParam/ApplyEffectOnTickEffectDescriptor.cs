using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyEffectOnTickEffectDescriptor : EffectDescriptor
{
    public bool tickOnStart = false;
    public float tickCoolDown = 1;
    public EffectDescriptor[] effectsToApply;
    public float effectDuration = 1;

    public override Effect getNewEffect()
    {
        ApplyEffectOnTickEffect effect = new ApplyEffectOnTickEffect(effectName, true, effectDuration, tickCoolDown, effectsToApply);

        effect.tickOnStart = tickOnStart;

        return effect;
    }
}
