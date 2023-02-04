using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WarningZoneAnimator : ZoneAnimator
{
    public override bool updateAnimation()
    {
        bool keepAnimating = base.updateAnimation();
        zoneMaterial.SetFloat("_RemainingTimeHint", deltaTimeCounter / animationDuration);

        return keepAnimating;
    }
}
