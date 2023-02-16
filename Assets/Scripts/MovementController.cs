using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public LayerMask obstaclesLayers;
    public float speed;
    public float rotateSpeed = 5;

    public float maxRotationMagnitude = 100;
    public int frameToSmooth = 5;
    private Vector2[] lastRotations;
    private int lastRotationsIndex = 0;

    private Vector2 rotation = Vector2.zero;
    public Vector2 rotationVerticalClamps;
    public Vector2 rotationHorizontalClamps;

    public Transform cameraHolder;

    public CharacterStats associatedCharacter;

    private void Start()
    {
        lastRotations = new Vector2[frameToSmooth];
        for (int i = 0; i < lastRotations.Length; i++)
        {
            lastRotations[i] = Vector2.zero;
        }
    }

    public void updateMovements(Vector2 local2DRotation, Vector2 local2DTranslation)
    {
        updateRotations(local2DRotation);
        updateTranslations(local2DTranslation);
    }

    private void updateRotations(Vector2 seeMovement)
    {
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

    private void updateTranslations(Vector2 requested2DMovement)
    {
        Vector3 movement3D = new Vector3(requested2DMovement.x, 0, requested2DMovement.y) * speed * Time.deltaTime;

        Vector3 worldMovement = transform.localToWorldMatrix * movement3D;
        float worldMovementMagnitude = worldMovement.magnitude;

        //checkCollisions
        if (Physics.Raycast(transform.position, worldMovement, out RaycastHit hit, worldMovementMagnitude * 10, obstaclesLayers))
        {
            //Hit a wall, start gliding alongside it
            Vector3 wallNormal = hit.normal;
            float angle = Vector3.Angle(wallNormal, worldMovement);
            Vector3 projectedMovementOnNormal = worldMovementMagnitude * Mathf.Cos(Mathf.Deg2Rad * angle) * wallNormal.normalized;
            worldMovement = worldMovement - projectedMovementOnNormal;
        }

        transform.position += worldMovement;

        associatedCharacter.notifyPlayerMovementChange(requested2DMovement.sqrMagnitude > 0);
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
