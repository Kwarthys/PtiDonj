using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTargetingData
{
    public CharacterStats charDidHit = null;
    public bool characterHit;
    public Vector3 pointHit;
    public bool groundHit;

    public void registerGroundUnderCharacter()
    {
        if (charDidHit == null)
        {
            Debug.LogError("Trying to register ground under null character.");
            return;
        }

        if(tryFindGroundUnder(charDidHit.transform.position, out Vector3 floorHit))
        {
            pointHit = floorHit;
            groundHit = true;
        }
        groundHit = false;
    }

    public static bool tryFindGroundUnder(Vector3 worldPos, out Vector3 groundWorldPos)
    {
        groundWorldPos = Vector3.zero;

        if (Physics.Raycast(worldPos + Vector3.up, Vector3.down, out RaycastHit floorhit, 100, LocalReferencer.instance.groundLayer)) //looking for floor
        {
            groundWorldPos = floorhit.point;

            return true;
        }

        return false;
    }
}
