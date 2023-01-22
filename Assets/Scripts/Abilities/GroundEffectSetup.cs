using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEffectSetup : MonoBehaviour
{
    public enum MarkerType { damagingZone, WarningZone}

    public MarkerType markerType;

    private Transform groundModelsHolder;

    public EffectDescriptor mainEffect;

    public Effect spawnedEffect = null;

    private void Start()
    {
        setupGroundMarkers();
    }

    public void setupGroundMarkers()
    {
        GameObject holder = new GameObject("ModelsHolder");
        groundModelsHolder = holder.transform;
        groundModelsHolder.parent = transform;
        groundModelsHolder.localPosition = Vector3.zero;

        GameObject markerPrefab = getGroundMarker();

        GameObject marker = Instantiate(markerPrefab, spawnedEffect.effectWorldPos + Vector3.up * 0.01f, markerPrefab.transform.rotation, groundModelsHolder);

        ColliderTriggerHandler trigger = marker.GetComponent<ColliderTriggerHandler>();
        WarningZoneAnimator animator = marker.GetComponent<WarningZoneAnimator>();

        if(animator != null)
        {
            animator.animationDuration = ((OnTimeEffect)spawnedEffect).effectDuration;
            animator.associatedGameObject = gameObject;
            GameManager.instance.registerAnimatedObject(animator);
        }

        IColliderEffect colliderEffect = (IColliderEffect)spawnedEffect;

        colliderEffect.registerColliderTrigger(trigger);
        float zoneSize = colliderEffect.getZoneSize();

        GameObject hint = Instantiate(GameManager.instance.groundZonePositionHintPrefab, groundModelsHolder);

        marker.transform.localScale = new Vector3(zoneSize, zoneSize, 1);
        hint.transform.localScale = new Vector3(zoneSize, 1, zoneSize);
        hint.transform.position = spawnedEffect.effectWorldPos;
    }

    private GameObject getGroundMarker()
    {
        switch(markerType)
        {
            case MarkerType.damagingZone:
                return GameManager.instance.groundDamagingZoneMarkerPrefab;

            case MarkerType.WarningZone:
            default:
                return GameManager.instance.groundWarningZoneMarkerPrefab;
        }
    }
}
