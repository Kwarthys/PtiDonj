using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDamageEffectDescritor : EffectDescriptor
{
    public float damage = 10;

    public override Effect getNewEffect()
    {
        return new InstantDamageEffect(effectName, true,damage);
    }
}
