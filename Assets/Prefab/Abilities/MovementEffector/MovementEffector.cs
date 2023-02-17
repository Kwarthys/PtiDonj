using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementEffector : MonoBehaviour
{
    public bool locksInputs = true;

    /// <summary>
    /// 
    /// </summary>
    /// <returns>False if movement is over</returns>
    public abstract bool updateMovement();

    public abstract void setupMovement(CharacterStats ownerStats);

}
