using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTargetingData
{
    public bool didHit;
    public Vector3 pointHit;
    public CharacterStats charHit = null;

    public bool registerGroundUnder(CharacterStats player)
    {
        if(tryFindGroundUnder(player.transform.position, out Vector3 floorHit))
        {
            pointHit = floorHit;
            return true;
        }
        return false;
    }

    public static bool tryFindGroundUnder(Vector3 worldPos, out Vector3 groundWorldPos)
    {
        groundWorldPos = Vector3.zero;

        if (Physics.Raycast(worldPos + Vector3.up, Vector3.down, out RaycastHit floorhit, 100, GameManager.instance.groundLayer)) //looking for floor
        {
            groundWorldPos = floorhit.point;

            return true;
        }

        return false;
    }
}
