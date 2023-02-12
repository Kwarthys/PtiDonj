using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovementController : MonoBehaviour
{
    public enum MovementMode {RoundRobin, Random}

    public Transform[] waypoints;
    public MovementMode movementMode;

    public void updateMovement()
    {

    }
}
