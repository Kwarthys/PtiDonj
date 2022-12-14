using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotDamageEffectDescriptor : EffectDescriptor
{
    public float tickDamage = 10;
    public float effectDuration = 5;
    public float effectTickCooldown = .5f;

    public override Effect getNewEffect()
    {
        return new DotDamageEffect(tickDamage, effectDuration, effectTickCooldown);
    }
}
