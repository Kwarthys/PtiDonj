using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotDamageEffect : TickingEffect
{
    public float tickDamage;

    public DotDamageEffect(float tickDamage, float effectDuration, float effectTickCooldown)
    {
        this.tickDamage = tickDamage;
        this.effectDuration = effectDuration;
        this.effectTickCooldown = effectTickCooldown;

        this.effectOnDuration = true;
    }

    public override void onTick()
    {
        inflictDamage(tickDamage);
    }
}
