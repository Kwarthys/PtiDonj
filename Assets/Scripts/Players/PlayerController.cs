using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 lastMovement = Vector2.zero;

    public float speed = 10;
    public float rotateSpeed = 5;

    public float maxRotationMagnitude = 100;
    public int frameToSmooth = 5;
    private Vector2[] lastRotations;
    private int lastRotationsIndex = 0;

    private Vector2 rotation = Vector2.zero;
    public Vector2 rotationVerticalClamps;
    public Vector2 rotationHorizontalClamps;

    public LayerMask obstaclesLayers;

    public Transform cameraHolder;

    public CharacterStats associatedPlayer;

    private void Start()
    {
        lastRotations = new Vector2[frameToSmooth];
        for (int i = 0; i < lastRotations.Length; i++)
        {
            lastRotations[i] = Vector2.zero;
        }
    }

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

            seeMovement = smoothMovement(seeMovement);

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

        Vector3 worldMovement = transform.localToWorldMatrix * movement3D;

        //checkCollisions
        Debug.DrawRay(transform.position, worldMovement * 3, Color.black, 3);

        if(Physics.Raycast(transform.position, worldMovement, out RaycastHit hit, worldMovement.magnitude * 6, obstaclesLayers))
        {
            //Debug.Log("Hit " + hit.transform.name);
            //Debug.DrawRay(hit.point, hit.normal, Color.blue, 5);
        }
        else
        {
            transform.Translate(movement3D);
        }
        
        associatedPlayer.moving = lastMovement.sqrMagnitude > 0;
    }

    private Vector2 smoothMovement(Vector2 lastSeeMovement)
    {
        if (lastSeeMovement.sqrMagnitude > maxRotationMagnitude * maxRotationMagnitude)
        {
            lastSeeMovement = lastSeeMovement.normalized * maxRotationMagnitude;
        }

        lastRotations[lastRotationsIndex] = lastSeeMovement;
        lastRotationsIndex = (lastRotationsIndex + 1) % lastRotations.Length;

        Vector2 smoothedMovement = Vector2.zero;

        for (int i = 0; i < lastRotations.Length; i++)
        {
            smoothedMovement += lastRotations[i];
        }

        smoothedMovement /= lastRotations.Length;

        return smoothedMovement;
    }
}
