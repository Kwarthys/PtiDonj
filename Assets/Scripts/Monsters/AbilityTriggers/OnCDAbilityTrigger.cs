using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCDAbilityTrigger : MonsterAbilityTrigger
{
    public override bool isTriggerValid()
    {
        return linkedAbility.canCast();
    }
}
