using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GroundEffectSetup : NetworkBehaviour
{
    public enum MarkerType { damagingZone, WarningZone, WindZone}

    public MarkerType markerType;

    private Transform groundModelsHolder;

    public EffectDescriptor mainEffect;

    private Effect spawnedEffect = null;

    public void initialize(Effect spawnedEffect)
    {
        if(isServer)
        {
            this.spawnedEffect = spawnedEffect;
            float effectDuration = ((OnTimeEffect)spawnedEffect).effectDuration;
            IColliderEffect colliderEffect = (IColliderEffect)spawnedEffect;
            float zoneSize = colliderEffect.getZoneSize();

            ClientRPCSetupGroundMarkers(effectDuration, zoneSize, transform.position);
        }
        else
        {
            Debug.LogWarning("Initialize GroundEffectSetup client side, should only be called server-side");
        }
    }

    [ClientRpc]
    public void ClientRPCSetupGroundMarkers(float effectDuration, float zoneSize, Vector3 pos)
    {
        GameObject holder = new GameObject("ModelsHolder");
        groundModelsHolder = holder.transform;
        groundModelsHolder.parent = LocalReferencer.instance.groundZoneMarkersHolder;
        groundModelsHolder.position = pos;

        GameObject markerPrefab = getGroundMarker();

        GameObject marker = Instantiate(markerPrefab, pos + Vector3.up * 0.01f, markerPrefab.transform.rotation, groundModelsHolder);

        ColliderTriggerHandler trigger = marker.GetComponent<ColliderTriggerHandler>();
        ZoneAnimator animator = marker.GetComponent<ZoneAnimator>();

        if(animator != null)
        {
            animator.initialize(effectDuration, holder);
            LocalAnimatorManager.instance.registerAnimatedLocalObject(animator);

            if(animator.playOnStartFXPrefab != null)
            {
                GameObject vfx = Instantiate(animator.playOnStartFXPrefab, pos + Vector3.up * 0.01f, Quaternion.identity, LocalReferencer.instance.groundZoneMarkersHolder);
                DamagingFXController fxController = vfx.GetComponent<DamagingFXController>();
                fxController.initialize(zoneSize, effectDuration);
                LocalAnimatorManager.instance.registerAnimatedLocalObject(fxController);
            }

            if(animator.onEndFXPrefab != null)
            {
                GameObject vfx = Instantiate(animator.onEndFXPrefab, pos + Vector3.up * 0.01f, Quaternion.identity, LocalReferencer.instance.groundZoneMarkersHolder);
                ExplosionVFXController fxController = vfx.GetComponent<ExplosionVFXController>();
                fxController.setupExplosion(zoneSize, effectDuration, 3);
                LocalAnimatorManager.instance.registerAnimatedLocalObject(fxController);
            }
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

            case MarkerType.WindZone:
                return LocalReferencer.instance.windZoneMarkerPrefab;

            case MarkerType.WarningZone:
            default:
                return LocalReferencer.instance.groundWarningZoneMarkerPrefab;
        }
    }
}
