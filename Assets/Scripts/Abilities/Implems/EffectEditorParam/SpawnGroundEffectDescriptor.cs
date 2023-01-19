using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGroundEffectDescriptor : EffectDescriptor
{
    public GameObject groundEffectPrefab;

    public override Effect getNewEffect()
    {
        SpawnGroundEffectEffect effect = new SpawnGroundEffectEffect(effectName, false, groundEffectPrefab);

        return effect;
    }
}
