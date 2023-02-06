using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ZoneAnimator : MonoBehaviour, IMyAnimator
{
    protected Material zoneMaterial;

    [HideInInspector]
    public float animationDuration;
    protected float deltaTimeCounter { get; private set; } = 0;

    [HideInInspector]
    public GameObject associatedGameObject;

    public GameObject playOnStartFXPrefab;
    public GameObject onEndFXPrefab;

    public void destroyAnimator()
    {
        Destroy(associatedGameObject);
    }

    public virtual bool updateAnimation()
    {
        deltaTimeCounter += Time.deltaTime;
        return deltaTimeCounter / animationDuration < 1;
    }

    public void initialize(float animationDuration, GameObject associatedGameObject)
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        zoneMaterial = new Material(mr.material);
        mr.material = zoneMaterial;

        this.animationDuration = animationDuration;
        this.associatedGameObject = associatedGameObject;
    }
}