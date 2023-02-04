using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingZoneAnimator : ZoneAnimator
{
    public override bool updateAnimation()
    {
        bool keepAnimating = base.updateAnimation();
        return keepAnimating;
    }
}
