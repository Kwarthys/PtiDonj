using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 lastMovement = Vector2.zero;

    public float speed = 10;
    public float rotateSpeed = 5;

    private Vector2 rotation = Vector2.zero;
    public Vector2 rotationVerticalClamps;
    public Vector2 rotationHorizontalClamps;

    public Transform cameraHolder;

    private void OnConnectedToServer()
    {
        Debug.Log("connected to server");
    }

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
        Vector2 movement = context.ReadValue<Vector2>();
        lastMovement = movement;
    }

    public void onRotate(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            Vector2 seeMovement = context.ReadValue<Vector2>();
            //Debug.Log(seeMovement);

            rotation += seeMovement * speed * Time.deltaTime;

            if (rotationHorizontalClamps.sqrMagnitude != 0)
            {
                rotation.x = Mathf.Clamp(rotation.x, rotationHorizontalClamps.x, rotationHorizontalClamps.y);
            }

            if (rotationVerticalClamps.sqrMagnitude != 0)
            {
                rotation.y = Mathf.Clamp(rotation.y, rotationVerticalClamps.x, rotationVerticalClamps.y);
            }

            transform.localRotation = Quaternion.Euler(new Vector3(0, rotation.x, 0));

            cameraHolder.localRotation = Quaternion.Euler(new Vector3(-rotation.y, 0, 0));
        }
    }

    public void Update()
    {
        Vector3 movement3D = new Vector3(lastMovement.x, 0, lastMovement.y) * speed * Time.deltaTime;
        transform.Translate(movement3D);
    }
}
