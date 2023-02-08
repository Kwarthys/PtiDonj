using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFollowingPrefabEffect : Effect
{
    public GameObject prefabToSpawn;

    public SpawnFollowingPrefabEffect(string effectName, GameObject prefabToSpawn) : base(effectName, false)
    {
        this.prefabToSpawn = prefabToSpawn;
    }

    public override void onStart()
    {
        GameObject spawnedPrefab = GameManager.instance.spawnPrefab(prefabToSpawn, owner.followingZoneHolder);
        GroundEffectSetup setup = spawnedPrefab.GetComponent<GroundEffectSetup>();

        Effect e = setup.mainEffect.getNewEffect();
        e.caster = caster;
        e.associatedGameObject = spawnedPrefab;

        setup.initialize(e);

        owner.addEffect(e);
    }
}
