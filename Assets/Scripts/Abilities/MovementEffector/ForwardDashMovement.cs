using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardDashMovement : MovementEffector
{
    public float length = 1;
    public float speed = 1;

    private float movementDuration = -1;
    private float deltaTimeCounter = 0;

    private MovementController.MovementInputs nextInputs;

    public override bool updateMovement()
    {
        if(movementDuration == -1)
        {
            Debug.LogError("DashMovement not initialized");
            return false;
        }

        deltaTimeCounter += Time.deltaTime;

        float moveMagnitude = speed * Time.deltaTime;

        nextInputs = new MovementController.MovementInputs();
        nextInputs.local2DRotation = Vector2.zero;
        nextInputs.local2DTranslation = new Vector2(0, moveMagnitude);

        return deltaTimeCounter < movementDuration;
    }

    public override void setupEffector(CharacterStats ownerStats)
    {
        this.ownerStats = ownerStats;
        this.outputsMoveCommands = true;
    }

    public override void startMovement(AbilityTargetingData targeting)
    {
        //targeting not used by this simple dash
        movementDuration = length / speed; //advanced maths right here (yet i managed to mess it up)
        deltaTimeCounter = 0;
    }

    public override MovementController.MovementInputs getMoveCommands()
    {
        return nextInputs;
    }
}
