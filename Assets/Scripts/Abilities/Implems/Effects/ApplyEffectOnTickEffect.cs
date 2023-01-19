using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyEffectOnTickEffect : TickingEffect
{
    public EffectDescriptor[] effectsToApply;

    public ApplyEffectOnTickEffect(string effectName, bool effectOnDuration, float effectDuration, float effectTickCooldown, EffectDescriptor[] effectsToApply) : base(effectName, effectOnDuration, effectDuration, effectTickCooldown)
    {
        this.effectsToApply = effectsToApply;
    }

    public override void onTick()
    {
        applyEffectsTo(owner, effectsToApply);
    }
}
