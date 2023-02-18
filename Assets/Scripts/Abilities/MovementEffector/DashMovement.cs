using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMovement : MovementEffector
{
    public float lenght = 1;
    public Vector3 localDirection = new Vector3(0,0,1);
    public float speed = 1;

    private float movementDuration = -1;
    private float deltaTimeCounter = 0;

    public override bool updateMovement()
    {
        if(movementDuration == -1)
        {
            Debug.LogError("DashMovement not initialized");
            return false;
        }

        deltaTimeCounter += Time.deltaTime;

        float moveMagnitude = speed * Time.deltaTime;
        ownerStats.transform.Translate(localDirection * moveMagnitude);

        return deltaTimeCounter < movementDuration;
    }

    public override void setupEffector(CharacterStats ownerStats)
    {
        this.ownerStats = ownerStats;
    }

    public override void startMovement(AbilityTargetingData targeting)
    {
        //targeting not used by this simple dash
        movementDuration = lenght / speed; //advanced maths right here (yet i managed to mess this up)
        deltaTimeCounter = 0;
    }
}
