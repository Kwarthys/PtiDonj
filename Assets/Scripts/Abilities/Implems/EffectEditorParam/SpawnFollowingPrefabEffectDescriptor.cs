using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFollowingPrefabEffectDescriptor : EffectDescriptor
{
    public GameObject prefabToSpawn;

    public override Effect getNewEffect()
    {
        SpawnFollowingPrefabEffect effect = new SpawnFollowingPrefabEffect(effectName, prefabToSpawn);

        return effect;
    }
}
