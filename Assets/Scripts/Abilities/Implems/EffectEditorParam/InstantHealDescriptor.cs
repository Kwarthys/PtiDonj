using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantHealDescriptor : EffectDescriptor
{
    public float heal = 30;

    public override Effect getNewEffect()
    {
        return new InstantHealEffect(effectName, true, heal);
    }
}
