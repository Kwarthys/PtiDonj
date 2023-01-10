using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfAbility : Ability
{
    protected override AbilityTargetingData computeTargeting()
    {
        AbilityTargetingData targeting = new AbilityTargetingData();

        targeting.didHit = true;
        targeting.charHit = manager.selfStats;
        targeting.registerGroundUnder(manager.selfStats);

        return targeting;
    }
}
