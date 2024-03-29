using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalReferencer : MonoBehaviour
{
    public static LocalReferencer instance;

    public GameObject groundZonePositionHintPrefab;
    public GameObject groundDamagingZoneMarkerPrefab;
    public GameObject groundWarningZoneMarkerPrefab;
    public GameObject windZoneMarkerPrefab;
    public GameObject ExplosionVFXPrefab;
    public GameObject abilityWidgetPrefab;

    public Transform abilityWidgetsHolder;
    public Transform groundZoneMarkersHolder;

    public LayerMask groundLayer;
    public LayerMask obstaclesLayer;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    public LayerMask groundAndObstaclesLayers()
    {
        return groundLayer | obstaclesLayer;
    }
}
