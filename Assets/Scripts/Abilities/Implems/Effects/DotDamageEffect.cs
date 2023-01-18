using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotDamageEffect : TickingEffect
{
    public float tickDamage;

    public DotDamageEffect(float tickDamage, float effectDuration, float effectTickCooldown, bool tickOnStart)
    {
        this.tickDamage = tickDamage;
        this.effectDuration = effectDuration;
        this.effectTickCooldown = effectTickCooldown;
        this.tickOnStart = tickOnStart;

        this.effectOnDuration = true;
    }

    public override void onTick()
    {
        inflictDamage(tickDamage);
    }
}
