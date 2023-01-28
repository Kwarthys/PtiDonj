using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsDisplayManager : MonoBehaviour
{
    public LifeDisplayController lifeDisplayController;
    public CastBarDisplayController castBarDisplayController;

    protected bool needsOrientationUpdate = false;

    public void changeLifeDisplay(float lifePercent, float lifeReal, bool bypassAnimation = false)
    {
        lifeDisplayController.updateLifeDisplay(lifePercent, lifeReal, bypassAnimation);
    }

    public void setCastBar(float animationDuration, string text, CastBarDisplayController.FillMode fillMode)
    {
        castBarDisplayController.setupAnimationAndPlay(animationDuration, text, fillMode);
    }

    public void interruptCastBar()
    {
        castBarDisplayController.interruptCastBar();
    }

    public void updateDisplay()
    {
        if (lifeDisplayController.needsUpdate)
        {
            lifeDisplayController.updateDisplayAnimation();
        }

        if(castBarDisplayController.needsUpdate)
        {
            castBarDisplayController.updateAnimation();
        }

        if(needsOrientationUpdate)
        {
            updateDisplayOrientation();
        }
    }

    protected virtual void updateDisplayOrientation() { }
}
