using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGroundEffectDescriptor : EffectDescriptor
{
    public GameObject groundEffectPrefab;

    public override Effect getNewEffect()
    {
        return new SpawnGroundEffectEffect(groundEffectPrefab);
    }
}
