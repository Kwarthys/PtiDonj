using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WarningZoneAnimator : MonoBehaviour, IMyAnimator
{
    private Material zoneMaterial;

    [HideInInspector]
    public float animationDuration;
    private float deltaTimeCounter = 0;

    [HideInInspector]
    public GameObject associatedGameObject;

    public GameObject onEndFXPrefab;

    private static int debugCounter = 0;
    private string id = "";

    public void initialize(float animationDuration, GameObject associatedGameObject)
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        zoneMaterial = new Material(mr.material);
        mr.material = zoneMaterial;

        this.animationDuration = animationDuration;
        this.associatedGameObject = associatedGameObject;

        id = associatedGameObject.name + debugCounter;
        associatedGameObject.name = id;

        debugCounter++;
    }

    public void destroyAnimator()
    {
        Destroy(associatedGameObject);
    }

    public bool updateAnimation()
    {
        deltaTimeCounter += Time.deltaTime;
        zoneMaterial.SetFloat("_RemainingTimeHint", deltaTimeCounter / animationDuration);

        return deltaTimeCounter / animationDuration < 1;
    }
}
