using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningZoneAnimator : MonoBehaviour, IMyAnimator
{
    private Material zoneMaterial;

    public float animationDuration;
    private float deltaTimeCounter = 0;

    public GameObject associatedGameObject;

    private void Start()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        zoneMaterial = new Material(mr.material);
        mr.material = zoneMaterial;
    }

    void IMyAnimator.destroy()
    {
        //Destruction is NO LONGER handled by the attached effects, so we only instantiate the explosion animation AND DESTROY EFFECT
        GameObject vfx = Instantiate(GameManager.instance.ExplosionVFXPrefab, transform.position, Quaternion.identity);
        ExplosionVFXController vfxController = vfx.GetComponent<ExplosionVFXController>();
        vfxController.setExplosionSize(transform.localScale.x);

        if(associatedGameObject != null)
        {
            Destroy(associatedGameObject);
        }
    }


    bool IMyAnimator.updateAnimation()
    {
        deltaTimeCounter += Time.deltaTime;

        zoneMaterial.SetFloat("_RemainingTimeHint", deltaTimeCounter / animationDuration);

        return deltaTimeCounter / animationDuration < 1;
    }
}
