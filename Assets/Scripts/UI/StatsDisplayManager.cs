using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsDisplayManager : MonoBehaviour
{
    public LifeDisplayController lifeDisplayController;

    protected bool needsOrientationUpdate = false;

    public void changeLifeDisplay(float lifePercent, float lifeReal, bool bypassAnimation = false)
    {
        lifeDisplayController.updateLifeDisplay(lifePercent, lifeReal, bypassAnimation);
    }

    public void updateDisplay()
    {
        if (lifeDisplayController.needsUpdate)
        {
            lifeDisplayController.updateDisplayAnimation();
        }

        if(needsOrientationUpdate)
        {
            updateDisplayOrientation();
        }
    }

    protected virtual void updateDisplayOrientation() { }
}
