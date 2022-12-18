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

    public DotDamageEffect(float tickDamage, float effectDuration, float effectTickCooldown)
    {
        this.tickDamage = tickDamage;
        this.effectDuration = effectDuration;
        this.effectTickCooldown = effectTickCooldown;

        this.effectOnDuration = true;
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

    public override bool onTick()
    {
        if(Time.realtimeSinceStartup - effectLastTick > effectTickCooldown)
        {
            owner.takeDamage(tickDamage);
            effectLastTick = Time.realtimeSinceStartup;
        }

        if(Time.realtimeSinceStartup - effectStart > effectDuration + effectTickCooldown/2) //making sure last tick will always proc
        {
            return false; //effect ends, returning false will remove it from the active effects of the owner (and call onEnd())
        }

        return true;

    }
}
