using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 lastTranslation { get; private set; } = Vector2.zero;
    public Vector2 lastRotation { get; private set; } = Vector2.zero;

    public void onJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Jump (will see that later)");
        }
    }

    public void onMove(InputAction.CallbackContext context)
    {
        lastTranslation = context.ReadValue<Vector2>();
    }

    public void onRotate(InputAction.CallbackContext context)
    {
        lastRotation = context.ReadValue<Vector2>();
    }

    public MovementController.MovementInputs getInputs()
    {
        MovementController.MovementInputs inputs = new MovementController.MovementInputs();
        inputs.local2DRotation = lastRotation;
        inputs.local2DTranslation = lastTranslation;

        return inputs;
    }
}
