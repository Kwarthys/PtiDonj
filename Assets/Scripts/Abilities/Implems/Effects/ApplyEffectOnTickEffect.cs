using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyEffectOnTickEffect : TickingEffect
{
    public EffectDescriptor[] effectsToApply;

    public ApplyEffectOnTickEffect(string effectName, float effectDuration, float effectTickCooldown, EffectDescriptor[] effectsToApply) : base(effectName, effectDuration, effectTickCooldown)
    {
        this.effectsToApply = effectsToApply;
    }

    public override void onTick()
    {
        applyEffectsTo(owner, effectsToApply);
    }
}
