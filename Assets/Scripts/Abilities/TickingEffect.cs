using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TickingEffect : OnTimeEffect
{

    public float effectTickCooldown;

    public bool tickOnStart = false;

    private int tickHappened;

    protected override bool updateEffect()
    {
        deltaTimeCounter += Time.deltaTime;

        while (deltaTimeCounter > effectTickCooldown * tickHappened) //while instead of if allows to tick multiple times for fast ticks and low updates count
        {
            onTick();
            tickHappened++;
        }

        if (deltaTimeCounter > effectDuration)
        {
            return false; //effect ends, returning false will remove it from the active effects of the owner
        }

        return true;
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
