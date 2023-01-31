using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GroundEffectSetup : NetworkBehaviour
{
    public enum MarkerType { damagingZone, WarningZone}

    public MarkerType markerType;

    private Transform groundModelsHolder;

    public EffectDescriptor mainEffect;

    private Effect spawnedEffect = null;

    public bool gameManagerAutoRemove = true;

    public void initialize(Effect spawnedEffect)
    {
        if(isServer)
        {
            this.spawnedEffect = spawnedEffect;
            float effectDuration = ((OnTimeEffect)spawnedEffect).effectDuration;
            IColliderEffect colliderEffect = (IColliderEffect)spawnedEffect;
            float zoneSize = colliderEffect.getZoneSize();

            ClientRPCSetupGroundMarkers(effectDuration, zoneSize);

            spawnedEffect.associatedGameObject = gameManagerAutoRemove ? gameObject : null;
        }
    }

    [ClientRpc]
    public void ClientRPCSetupGroundMarkers(float effectDuration, float zoneSize)
    {
        GameObject holder = new GameObject("ModelsHolder");
        groundModelsHolder = holder.transform;
        groundModelsHolder.parent = transform;
        groundModelsHolder.localPosition = Vector3.zero;

        GameObject markerPrefab = getGroundMarker();

        GameObject marker = Instantiate(markerPrefab, transform.position + Vector3.up * 0.01f, markerPrefab.transform.rotation, groundModelsHolder);

        ColliderTriggerHandler trigger = marker.GetComponent<ColliderTriggerHandler>();
        WarningZoneAnimator animator = marker.GetComponent<WarningZoneAnimator>();

        if(animator != null)
        {
            animator.initialize(effectDuration, gameObject);
            LocalAnimatorManager.instance.registerAnimatedLocalObject(animator);
        }

        if(isServer) //spawnedEffect will be null on clients, where we don't need that link
        {
            IColliderEffect colliderEffect = (IColliderEffect)spawnedEffect;
            colliderEffect.registerColliderTrigger(trigger);
        }

        GameObject hint = Instantiate(LocalReferencer.instance.groundZonePositionHintPrefab, groundModelsHolder);
        ZonePosHintController hintController = hint.GetComponent<ZonePosHintController>();
        hintController.initialize();
        trigger.associatedHintZoneController = hintController;

        marker.transform.localScale = new Vector3(zoneSize, zoneSize, 1);
        hint.transform.localScale = new Vector3(zoneSize, 1, zoneSize);
        hint.transform.position = transform.position;
    }

    private GameObject getGroundMarker()
    {
        switch(markerType)
        {
            case MarkerType.damagingZone:
                return LocalReferencer.instance.groundDamagingZoneMarkerPrefab;

            case MarkerType.WarningZone:
            default:
                return LocalReferencer.instance.groundWarningZoneMarkerPrefab;
        }
    }
}
