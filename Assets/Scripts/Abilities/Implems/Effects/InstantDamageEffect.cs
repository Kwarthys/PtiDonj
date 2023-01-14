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

    public InstantDamageEffect(float damage)
    {
        this.damage = damage;
    }
}
