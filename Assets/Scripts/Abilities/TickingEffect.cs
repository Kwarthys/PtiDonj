using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TickingEffect : OnTimeEffect
{

    public float effectTickCooldown;
    protected float effectLastTick = -1;

    public bool tickOnStart = false;

    protected override bool updateEffect()
    {
        if (Time.realtimeSinceStartup - effectLastTick > effectTickCooldown)
        {
            onTick();
            effectLastTick = Time.realtimeSinceStartup;
        }

        if (Time.realtimeSinceStartup - effectStart > effectDuration + effectTickCooldown / 2) //making sure last tick will always trigger
        {
            onEnd();
            return false; //effect ends, returning false will remove it from the active effects of the owner
        }

        return true;
    }
    public override void onStart()
    {
        effectStart = Time.realtimeSinceStartup;

        effectLastTick = effectStart;

        if (tickOnStart)
        {
            effectLastTick -= effectTickCooldown; //That way we generate a tick instantly
        }
    }
}
