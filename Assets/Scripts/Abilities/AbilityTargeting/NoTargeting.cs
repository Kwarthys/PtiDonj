using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoTargeting : AbilityTargeting
{
    public override AbilityTargetingData[] findTargets(LayerMask targetsLayerMask)
    {
        return null;
    }
}
