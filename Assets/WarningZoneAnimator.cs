using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningZoneAnimator : MonoBehaviour, IMyAnimator
{
    private Material zoneMaterial;

    public float animationDuration;
    private float deltaTimeCounter = 0;

    private void Start()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        zoneMaterial = new Material(mr.material);
        mr.material = zoneMaterial;
    }

    void IMyAnimator.destroy() {} //This is handled by the gameobject itself

    bool IMyAnimator.updateAnimation()
    {
        deltaTimeCounter += Time.deltaTime;

        zoneMaterial.SetFloat("_RemainingTimeHint", deltaTimeCounter / animationDuration);

        return deltaTimeCounter / animationDuration < 1;
    }
}
