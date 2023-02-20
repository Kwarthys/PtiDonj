using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementEffector : MonoBehaviour
{
    protected CharacterStats ownerStats;
    public bool locksInputs = true;

    /// <summary>
    /// 
    /// </summary>
    /// <returns>False if movement is over</returns>
    public abstract bool updateMovement();

    public abstract void setupEffector(CharacterStats ownerStats);

    public abstract void startMovement(AbilityTargetingData targeting);

 
    public bool outputsMoveCommands { get; protected set; } = false;
    public virtual MovementController.MovementInputs getMoveCommands()
    {
        Debug.LogWarning("GetMoveCommands not implemented, returning empty commands");
        return new MovementController.MovementInputs();
    }

}
