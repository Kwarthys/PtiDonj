using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMovement : MovementEffector
{
    public float lenght = 1;
    public Vector3 localDirection;
    public float speed = 1;

    [HideInInspector]
    public CharacterStats ownerStats;

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

    public override void setupMovement(CharacterStats ownerStats)
    {
        this.ownerStats = ownerStats;

        deltaTimeCounter = 0;
        movementDuration = speed / lenght; //advanced maths right here
    }
}
