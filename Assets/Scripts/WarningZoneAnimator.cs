using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningZoneAnimator : MonoBehaviour, IMyAnimator
{
    private Material zoneMaterial;

    public float animationDuration;
    private float deltaTimeCounter = 0;

    private GameObject associatedGameObject;

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

        Debug.Log("Setup " + associatedGameObject + " (" + id + ")");
    }

    void IMyAnimator.destroy()
    {
        Debug.Log("Destroying " + associatedGameObject + " (" + id + ")");
        //Destruction is NO LONGER handled by the attached effects, so we only instantiate the explosion animation AND DESTROY EFFECT
        GameObject vfx = Instantiate(GameManager.instance.ExplosionVFXPrefab, transform.position, Quaternion.identity);
        ExplosionVFXController vfxController = vfx.GetComponent<ExplosionVFXController>();
        vfxController.setExplosionSize(transform.localScale.x);

        if(associatedGameObject != null)
        {
            Debug.Log("Destroyed " + associatedGameObject + " (" + id + ")");
            Destroy(associatedGameObject);
        }
        else
        {
            Debug.LogWarning("null associatedGameObject");
        }
    }

    private void OnDestroy()
    {
        Debug.Log("ONDESTROY on " + associatedGameObject + " (" + id + ")");
    }


    bool IMyAnimator.updateAnimation()
    {
        if(associatedGameObject == null)
        {
            Debug.Log("Updating " + associatedGameObject + " (" + id + ")");
        }

        deltaTimeCounter += Time.deltaTime;

        zoneMaterial.SetFloat("_RemainingTimeHint", deltaTimeCounter / animationDuration);

        return deltaTimeCounter / animationDuration < 1;
    }
}
