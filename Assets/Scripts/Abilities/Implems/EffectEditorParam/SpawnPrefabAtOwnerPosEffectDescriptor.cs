using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefabAtOwnerPosEffectDescriptor : EffectDescriptor
{
    public GameObject prefab;
    public override Effect getNewEffect()
    {
        return new SpawnPrefabAtOwnerPosEffect(effectName, prefab);
    }
}
