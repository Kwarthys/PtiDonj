using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlayerAbility : Ability
{
    protected override AbilityTargetingData computeTargeting()
    {
        AbilityTargetingData targeting = new AbilityTargetingData();
        targeting.didHit = false;

        CharacterStats player = PlayerManager.instance.getRandomCharacter();
        if (player == null)
        {
            return targeting;
        }

        targeting.charHit = player;

        targeting.didHit = targeting.registerGroundUnder(player);

        return targeting;
    }
}
