using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FollowingEffectSetup : InstanciatedEffectSetup
{
    public override void setupMarkers(float effectDuration, float zoneSize, uint parentID)
    {
        Transform parent = PlayerManager.instance.getCharacter(parentID).followingZoneHolder;

        GameObject marker = Instantiate(getGroundMarker(), parent);

        Debug.Log(parent);

        ColliderTriggerHandler trigger = marker.GetComponent<ColliderTriggerHandler>();

        ZoneAnimator animator = marker.GetComponent<ZoneAnimator>();

        if (animator != null)
        {
            animator.initialize(effectDuration, marker);
            LocalAnimatorManager.instance.registerAnimatedLocalObject(animator);

            if (animator.playOnStartFXPrefab != null)
            {
                GameObject vfx = Instantiate(animator.playOnStartFXPrefab, parent);
                vfx.transform.localPosition = Vector3.zero; //dunno why this is needed
                VFXController fxController = vfx.GetComponent<VFXController>();
                fxController.initialize(zoneSize, effectDuration);
                LocalAnimatorManager.instance.registerAnimatedLocalObject(fxController);
            }
        }

        if (isServer) //spawnedEffect will be null on clients, where we don't need that link
        {
            IColliderEffect colliderEffect = (IColliderEffect)spawnedEffect;
            colliderEffect.registerColliderTrigger(trigger);
        }

        marker.transform.localScale = new Vector3(zoneSize, zoneSize, 1);
    }
}
