using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTargetingData
{
    public CharacterStats characterHit = null;
    public bool charDidHit;
    public Vector3 pointHit;
    public bool groundDidHit;

    public void registerGroundUnderCharacter()
    {
        if (characterHit == null)
        {
            Debug.LogError("Trying to register ground under null character.");
            return;
        }

        groundDidHit = false;
        if(tryFindGroundUnder(characterHit.transform.position, out Vector3 floorHit))
        {
            pointHit = floorHit;
            groundDidHit = true;
        }
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
