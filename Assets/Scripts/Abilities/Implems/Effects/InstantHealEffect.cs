using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantHealEffect : Effect
{
    public float heal;

    public InstantHealEffect(string name, bool effectOnDuration, float heal) : base(name, effectOnDuration)
    {
        this.heal = heal;
    }

    public override void onStart()
    {
        sendHealing(heal);
    }
}
