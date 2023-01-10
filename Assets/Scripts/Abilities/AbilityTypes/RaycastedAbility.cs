using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastedAbility : Ability
{    
    protected override AbilityTargetingData computeTargeting()
    {
        AbilityTargetingData result = new AbilityTargetingData();

        result.didHit = false;

        Transform visor = manager.visor;

        Debug.DrawRay(visor.position, visor.forward * 10, Color.black, 3);

        if (Physics.Raycast(visor.position, visor.forward, out RaycastHit hit, 250, targetLayer | GameManager.instance.groundLayer))
        {
            result.didHit = true;
            result.charHit = hit.transform.GetComponent<CharacterStats>();
            result.pointHit = new Vector3(0, 0, 0); //this should always be updated, if didHit is true

            drawDebugCrossAtPoint(hit.point);

            if (Physics.Raycast(hit.point + Vector3.up, Vector3.down, out RaycastHit floorhit, 100, GameManager.instance.groundLayer)) //looking for floor
            {
                result.pointHit = floorhit.point;
            }
            else
            {
                Debug.LogWarning("Did not find floor"); //not sure if this case will ever occur, tracking it just in case
            }

            //drawDebugCrossAtPoint(result.pointHit);
        }

        return result;
    }
}
