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
        if (Physics.Raycast(player.transform.position + Vector3.up, Vector3.down, out RaycastHit floorhit, 100, GameManager.instance.groundLayer)) //looking for floor
        {
            pointHit = floorhit.point;

            return true;
        }

        return false;
    }
}
