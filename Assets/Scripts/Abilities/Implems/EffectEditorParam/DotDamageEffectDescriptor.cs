using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotDamageEffectDescriptor : EffectDescriptor
{
    public float tickDamage = 10;
    public float effectDuration = 5;
    public float effectTickCooldown = .5f;
    public bool tickOnStart = false;

    public override Effect getNewEffect()
    {
        DotDamageEffect e =  new DotDamageEffect(effectName, true, effectDuration, effectTickCooldown, tickDamage);
        e.tickOnStart = tickOnStart;
        return e;
    }
}
