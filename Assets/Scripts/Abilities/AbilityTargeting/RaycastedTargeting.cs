using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastedTargeting : AbilityTargeting
{
    protected Transform visor;
    private void Start()
    {
        visor = GetComponentInParent<AbilityManager>().visor;
    }

    public override AbilityTargetingData[] findTargets(LayerMask targetsLayerMask)
    {
        AbilityTargetingData[] dataArray = new AbilityTargetingData[1]; //single target

        AbilityTargetingData result = new AbilityTargetingData();

        result.charDidHit = false;

        if (Physics.Raycast(visor.position, visor.forward, out RaycastHit hit, 250, targetsLayerMask | LocalReferencer.instance.groundLayer))
        {
            result.characterHit = hit.transform.GetComponent<CharacterStats>();
            result.charDidHit = result.characterHit != null;
            result.pointHit = new Vector3(0, 0, 0); //this should always be updated, if groundDidHit is true
            result.groundDidHit = false;

            if(AbilityTargetingData.tryFindGroundUnder(hit.point, out Vector3 point))
            {
                result.pointHit = point;
                result.groundDidHit = true;
            }
        }

        dataArray[0] = result;

        return dataArray;
    }
}
