using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHPTrigger : MonsterAbilityTrigger
{
    public float[] triggers;
    private int currentTriggerIndex = 0;

    public override bool isTriggerValid()
    {
        if (currentTriggerIndex >= triggers.Length) return false;

        float relativeLife = linkedAbility.ownerStats.getCurrentLifeRelative();

        if(relativeLife < triggers[currentTriggerIndex])
        {
            currentTriggerIndex++;
            return true;
        }

        return false;
    }
}
