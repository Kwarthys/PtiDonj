using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGroundEffectEffect : Effect
{
    public GameObject groundEffectPrefab;

    public SpawnGroundEffectEffect(GameObject groundEffectPrefab)
    {
        this.groundEffectPrefab = groundEffectPrefab;
    }

    public override void onStart()
    {
        GroundEffectSetup setup = GameManager.instance.spawnPrefab(groundEffectPrefab, effectWorldPos).GetComponent<GroundEffectSetup>();

        GameManager.instance.addGroundEffect(setup.mainEffect.getNewEffect());
    }
}
