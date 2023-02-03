using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlayersTargeting : AbilityTargeting
{
    public int numberOfTargets = 1;

    public override AbilityTargetingData[] findTargets(LayerMask targetsLayerMask)
    {
        CharacterStats[] targets = PlayerManager.instance.getNRandomCharacters(numberOfTargets);
        AbilityTargetingData[] dataArray = new AbilityTargetingData[targets.Length];

        for (int i = 0; i < targets.Length; i++)
        {
            AbilityTargetingData targeting = new AbilityTargetingData();
            targeting.characterHit = true;
            targeting.charDidHit = targets[i];
            targeting.registerGroundUnderCharacter();

            dataArray[i] = targeting;
        }

        return dataArray;
    }
}
