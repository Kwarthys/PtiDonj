using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TickingEffect : OnTimeEffect
{

    public float effectTickCooldown;

    public bool tickOnStart = false;

    private int tickHappened;

    public TickingEffect(string effectName, float effectDuration, float effectTickCooldown) : base(effectName, effectDuration)
    {
        this.effectTickCooldown = effectTickCooldown;
    }

    protected override bool updateEffect()
    {
        deltaTimeCounter += Time.deltaTime;

        while (deltaTimeCounter > effectTickCooldown * tickHappened) //while instead of if allows to tick multiple times for fast ticks and low updates count
        {
            onTick();
            tickHappened++;
        }

        return deltaTimeCounter < effectDuration; //returning false removes it from the active effects of the owner
    }
    public override void onStart()
    {
        deltaTimeCounter = 0;
        tickHappened = 1;

        if (tickOnStart)
        {
            tickHappened = 0;
        }
    }
}
