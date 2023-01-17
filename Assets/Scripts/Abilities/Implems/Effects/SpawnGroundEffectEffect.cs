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

        Effect e = setup.mainEffect.getNewEffect();
        e.caster = caster;
        e.effectWorldPos = effectWorldPos;
        GameManager.instance.addGroundEffect(e);

        setup.spawnedEffect = e;
    }
}
