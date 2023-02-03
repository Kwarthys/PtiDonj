using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityTargeting : MonoBehaviour
{
    public abstract AbilityTargetingData[] findTargets(LayerMask targetsLayerMask);
}
