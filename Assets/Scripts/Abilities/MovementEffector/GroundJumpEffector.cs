using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundJumpEffector : MovementEffector
{
    private Vector3 groundTarget;
    private Vector3 groundStart;

    public float jumpHeight = 1;
    public float jumpSpeed = 1;
    public bool speedIsJumpDuration = false;
    private float movementDuration;

    private float deltaTimeCounter = 0;

    public override void setupEffector(CharacterStats ownerStats)
    {
        this.ownerStats = ownerStats;

        locksRotation = false;
    }

    public override void startMovement(AbilityTargetingData targeting)
    {
        if(!targeting.groundDidHit)
        {
            Debug.LogError("targeting invalid - no ground hit");
            return;
        }
        
        groundTarget = targeting.groundHit;

        groundTarget += ownerStats.getFootBodyOffset();

        groundStart = ownerStats.transform.position;

        if(speedIsJumpDuration)
        {
            movementDuration = jumpSpeed;
        }
        else
        {
            movementDuration = Vector3.Distance(groundStart, groundTarget) / jumpSpeed;
        }

        deltaTimeCounter = 0;
    }

    public override bool updateMovement()
    {
        if (groundTarget == null) return false; //not initialized

        deltaTimeCounter += Time.deltaTime;

        float t = deltaTimeCounter / movementDuration;

        Vector3 pos = new Vector3();

        pos.x = groundStart.x + t * (groundTarget.x - groundStart.x);
        pos.y = groundStart.y + t * (groundTarget.y - groundStart.y) + parametricHeight(t);
        pos.z = groundStart.z + t * (groundTarget.z - groundStart.z);        

        if(t >= 1)
        {
            ownerStats.transform.position = groundTarget;
        }
        else
        {
            ownerStats.transform.position = pos;
        }

        return t < 1;
    }

    private float parametricHeight(float t)
    {
        //f(t) = -4 jumpHeigth t + 4 jumpHeigth t
        return -4 * jumpHeight * t * t + 4 * jumpHeight * t;
    }


    /***
     * f(t) = -4 jumpHeigth t + 4 jumpHeigth t
     * 
     * xt = groundStart.x + t * (groundTarget.x - groundStart.x)
     * yt = groundStart.y + t * (groundTarget.y - groundStart.y)
     * zt = groundStart.z + f(t) (groundTarget.z - groundStart.z)
     ***/
}
