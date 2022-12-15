using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotDamageEffect : Effect
{
    public float tickDamage;

    public float effectDuration;
    private float effectStart = -1;

    public float effectTickCooldown;
    private float effectLastTick = -1;

    public bool tickOnStart = false;

    public override void onStart()
    {
        effectStart = Time.realtimeSinceStartup;

        effectLastTick = effectStart;

        if (tickOnStart)
        {
            effectLastTick -= effectTickCooldown; //That way we generate a tick instantly
        }
    }

    public override bool onTick()
    {
        if(Time.realtimeSinceStartup - effectLastTick > effectTickCooldown)
        {
            owner.takeDamage(tickDamage);
            effectLastTick = Time.realtimeSinceStartup;
        }

        if(Time.realtimeSinceStartup - effectStart > effectDuration)
        {
            return false; //effect ends, returning false will remove it from the active effects of the owner (and call onEnd())
        }

        return true;

    }
}
