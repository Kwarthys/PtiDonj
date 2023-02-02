using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefabAtOwnerPosEffect : Effect
{
    public GameObject prefabToSpawn;

    public SpawnPrefabAtOwnerPosEffect(string effectName, GameObject prefab) : base(effectName, false)
    {
        this.prefabToSpawn = prefab;
    }

    public override void onStart()
    {
        if(AbilityTargetingData.tryFindGroundUnder(owner.transform.position, out Vector3 feetpos))
        {
            GroundEffectSetup setup = GameManager.instance.spawnPrefab(prefabToSpawn, feetpos).GetComponent<GroundEffectSetup>();

            Effect e = setup.mainEffect.getNewEffect();
            e.caster = caster;
            GameManager.instance.addGroundEffect(e);

            setup.initialize(e);
        }
        else
        {
            Debug.LogError("Can't spawn ground effect on owner's feet, ground not found");
        }

    }
}
