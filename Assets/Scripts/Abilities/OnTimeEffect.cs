using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTimeEffect : Effect
{
    public float effectDuration;
    protected float effectStart = -1;

    protected override bool updateEffect()
    {
        return Time.realtimeSinceStartup - effectStart <= effectDuration; //returning false removes it from the active effects of the owner
    }
    public override void onStart()
    {
        effectStart = Time.realtimeSinceStartup;
    }
}
