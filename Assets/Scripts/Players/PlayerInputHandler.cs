using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 lastTranslation = Vector2.zero;
    public Vector2 lastRotation = Vector2.zero;


    public void onJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Jump ");
        }
    }

    public void onMove(InputAction.CallbackContext context)
    {
        //x - horizontal // y-vertical
        lastTranslation = context.ReadValue<Vector2>();
    }

    public void onRotate(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            lastRotation = context.ReadValue<Vector2>();
        }
    }
}
