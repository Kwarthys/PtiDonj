using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EffectDescriptor : MonoBehaviour
{
    public string effectName = "Effect";
    public bool effectOnDuration = false;

    public abstract Effect getNewEffect();
}
