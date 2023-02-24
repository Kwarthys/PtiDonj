using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AbilityTargetingData
{
    public uint characterHitID;
    public bool charDidHit;
    public Vector3 groundHit;
    public bool groundDidHit;

    public void registerGroundUnderCharacter()
    {
        if (!charDidHit)
        {
            Debug.LogError("Trying to register ground under null character.");
            return;
        }

        CharacterStats character = PlayerManager.instance.getCharacter(characterHitID);
        groundDidHit = false;
        if(tryFindGroundUnder(character.transform.position, out Vector3 floorHit))
        {
            groundHit = floorHit;
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
