using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public abstract class InstanciatedEffectSetup : NetworkBehaviour
{
    public enum MarkerType { damagingZone, WarningZone, WindZone }
    public MarkerType markerType;

    [SerializeField]
    public EffectDescriptor mainEffect;

    protected Effect spawnedEffect = null;

    public void initialize(Effect spawnedEffect)
    {
        if (isServer)
        {
            this.spawnedEffect = spawnedEffect;
            float effectDuration = ((OnTimeEffect)spawnedEffect).effectDuration;
            IColliderEffect colliderEffect = (IColliderEffect)spawnedEffect;
            float zoneSize = colliderEffect.getZoneSize();

            uint parentID = 0;
            if (spawnedEffect.owner != null)
            {
                parentID = spawnedEffect.owner.netId;
            }

            RpcSetupMarkers(effectDuration, zoneSize, parentID);
        }
        else
        {
            Debug.LogWarning("Initialize Instantiated effect client-side, should only be called server-side");
        }
    }

    [ClientRpc]
    private void RpcSetupMarkers(float effectDuration, float zoneSize, uint parentCharacter)
    {
        setupMarkers(effectDuration, zoneSize, parentCharacter);
    }

    public abstract void setupMarkers(float effectDuration, float zoneSize, uint parentCharacter);

    protected GameObject getGroundMarker()
    {
        switch (markerType)
        {
            case MarkerType.WarningZone:
                return LocalReferencer.instance.groundWarningZoneMarkerPrefab;

            case MarkerType.WindZone:
                return LocalReferencer.instance.windZoneMarkerPrefab;

            case MarkerType.damagingZone:
            default:
                return LocalReferencer.instance.groundDamagingZoneMarkerPrefab;
        }
    }
}
