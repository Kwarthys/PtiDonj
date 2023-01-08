using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsDisplayManager : MonoBehaviour
{
    public LifeDisplayController lifeDisplayController;

    public void updateLifeDisplay(float lifePercent, float lifeReal)
    {
        lifeDisplayController.updateLifeDisplay(lifePercent, lifeReal);
    }

    public void updateDisplayOrientation()
    {
        if(GameManager.instance.localPlayerTransform != null)
        {
            transform.rotation = Quaternion.LookRotation(transform.position - GameManager.instance.localPlayerTransform.position);
        }
    }
}
