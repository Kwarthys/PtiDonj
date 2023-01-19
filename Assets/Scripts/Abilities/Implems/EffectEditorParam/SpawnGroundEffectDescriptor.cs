using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGroundEffectDescriptor : EffectDescriptor
{
    public GameObject groundEffectPrefab;
    public bool needsWorldPosUpdate = false; //needed if the floor is not directly targeted by the caasting ability (ie targeting a moving player)

    public override Effect getNewEffect()
    {
        SpawnGroundEffectEffect effect = new SpawnGroundEffectEffect(effectName, false, groundEffectPrefab);
        effect.needsWorldPosUpdate = needsWorldPosUpdate;
        return effect;
    }
}
