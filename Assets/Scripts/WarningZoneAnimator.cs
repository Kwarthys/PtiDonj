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
    }

    void IMyAnimator.destroyAnimator()
    {
        //Debug.Log("Destroying " + associatedGameObject + " (" + id + ")");
        //Destruction is NO LONGER handled by the attached effects, so we only instantiate the explosion animation AND DESTROY EFFECT

        if(associatedGameObject != null)
        {
            GameObject vfx = Instantiate(GameManager.instance.ExplosionVFXPrefab, transform.position, Quaternion.identity);
            ExplosionVFXController vfxController = vfx.GetComponent<ExplosionVFXController>();
            vfxController.setExplosionSize(transform.localScale.x);

            //Debug.Log("Destroyed " + associatedGameObject + " (" + id + ")");
            Destroy(associatedGameObject);
        }
        else
        {
            //this should not happen, but as it does, this will prevent errors
            Debug.LogWarning("null associatedGameObject while in IMyAnimator.destroy()");
        }
    }

    /*
    private void OnDestroy()
    {
        Debug.Log("ONDESTROY on " + associatedGameObject + " (" + id + ")");
    }*/


    bool IMyAnimator.updateAnimation()
    {
        /*
        bool sendDebug = false;
        string debug = "";

        if(associatedGameObject == null)
        {
            debug += ("Updating " + associatedGameObject + " (" + id + ")");
            sendDebug = true;
        }
        */

        deltaTimeCounter += Time.deltaTime;

        zoneMaterial.SetFloat("_RemainingTimeHint", deltaTimeCounter / animationDuration);
       
        /*
        if(!(deltaTimeCounter / animationDuration < 1))
        {
            if(sendDebug)
            {
                debug += " - ";
            }
            debug += ("Animation finished");
            sendDebug = true;
        }

        if(sendDebug)
        {
            Debug.Log(debug);
        }
        */
        return deltaTimeCounter / animationDuration < 1;
    }
}
