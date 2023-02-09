using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GroundEffectSetup : InstanciatedEffectSetup
{
    [ClientRpc]
    public override void setupMarkers(float effectDuration, float zoneSize, uint parentID)//parentID is not set and not used for ground effects
    {
        Vector3 pos = transform.position;

        GameObject holder = new GameObject("ModelsHolder");
        Transform groundModelsHolder = holder.transform;
        groundModelsHolder.parent = LocalReferencer.instance.groundZoneMarkersHolder;
        groundModelsHolder.position = pos;

        GameObject markerPrefab = getGroundMarker();

        GameObject marker = Instantiate(markerPrefab, pos + Vector3.up * 0.01f, markerPrefab.transform.rotation, groundModelsHolder);

        ZoneAnimator animator = marker.GetComponent<ZoneAnimator>();

        if(animator != null)
        {
            animator.initialize(effectDuration, holder);
            LocalAnimatorManager.instance.registerAnimatedLocalObject(animator);

            if(animator.playOnStartFXPrefab != null)
            {
                GameObject vfx = Instantiate(animator.playOnStartFXPrefab, pos + Vector3.up * 0.01f, Quaternion.identity, LocalReferencer.instance.groundZoneMarkersHolder);
                VFXController fxController = vfx.GetComponent<VFXController>();
                fxController.initialize(zoneSize, effectDuration);
                LocalAnimatorManager.instance.registerAnimatedLocalObject(fxController);
            }

            if(animator.onEndFXPrefab != null)
            {
                GameObject vfx = Instantiate(animator.onEndFXPrefab, pos + Vector3.up * 0.01f, Quaternion.identity, LocalReferencer.instance.groundZoneMarkersHolder);
                VFXController fxController = vfx.GetComponent<VFXController>();
                fxController.initialize(zoneSize, effectDuration);
                LocalAnimatorManager.instance.registerAnimatedLocalObject(fxController);
            }
        }

        ColliderTriggerHandler trigger = marker.GetComponent<ColliderTriggerHandler>();

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
}
