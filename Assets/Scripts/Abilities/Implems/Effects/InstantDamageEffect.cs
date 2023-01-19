using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDamageEffect : Effect
{
    public float damage = 10;

    public override void onStart()
    {
        inflictDamage(damage);
    }

    public InstantDamageEffect(string name, bool effectOnDuration, float damage) : base(name, effectOnDuration)
    {
        this.damage = damage;
    }
}
