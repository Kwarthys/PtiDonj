using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTimeEffect : Effect
{
    public float effectDuration;

    protected float deltaTimeCounter;

    protected override bool updateEffect()
    {
        deltaTimeCounter += Time.deltaTime;

        return deltaTimeCounter > effectDuration; //returning false removes it from the active effects of the owner
    }
    public override void onStart()
    {
        deltaTimeCounter = 0;
    }
}
