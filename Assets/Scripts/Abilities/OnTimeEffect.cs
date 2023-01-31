using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTimeEffect : Effect
{
    public float effectDuration;

    protected float deltaTimeCounter;

    public OnTimeEffect(string effectName, float effectDuration) : base(effectName, true)
    {
        this.effectDuration = effectDuration;
    }

    protected override bool updateEffect()
    {
        deltaTimeCounter += Time.deltaTime;
        return deltaTimeCounter < effectDuration; //returning false removes it from the active effects of the owner
    }
    public override void onStart()
    {
        deltaTimeCounter = 0;
    }
}
