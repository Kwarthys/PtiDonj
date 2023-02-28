using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyExplosionMovementEffectorEffectDescriptor : EffectDescriptor
{
    public float jumpLength;
    public float jumpHeigth;

    public override Effect getNewEffect()
    {
        ApplyExplosionMovementEffectorEffect effect = new ApplyExplosionMovementEffectorEffect(effectName, transform.position, jumpLength, jumpHeigth);

        return effect;
    }
}
