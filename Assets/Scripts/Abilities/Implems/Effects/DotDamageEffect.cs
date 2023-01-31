using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotDamageEffect : TickingEffect
{
    public float tickDamage;

    public DotDamageEffect(string effectName, float effectDuration, float effectTickCooldown, float tickDamage) : base(effectName, effectDuration, effectTickCooldown)
    {
        this.tickDamage = tickDamage;
    }

    public override void onTick()
    {
        inflictDamage(tickDamage);
    }
}
