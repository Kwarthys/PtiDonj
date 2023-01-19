using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotDamageEffect : TickingEffect
{
    public float tickDamage;

    public DotDamageEffect(string effectName, bool effectOnDuration, float effectDuration, float effectTickCooldown, float tickDamage) : base(effectName, effectOnDuration, effectDuration, effectTickCooldown)
    {
        this.tickDamage = tickDamage;
    }

    public override void onTick()
    {
        inflictDamage(tickDamage);
    }
}
