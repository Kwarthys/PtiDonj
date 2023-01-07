using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAbilityManager : MonoBehaviour
{
    public List<Ability> abilities;

    public void castAbilityOnRandomPlayer(int abilityIndex)
    {
        AbilityTargetingResult targeting = new AbilityTargetingResult();
        CharacterStats target = GameManager.instance.getRandomCharacter();

        targeting.didHit = true;
        targeting.charHit = target;
        targeting.pointHit = target.transform.position;

        //cast ability with this targeting stuff, this should be in the ability // monster ability classes

        //abilities[abilityIndex].tryCastAbility();   won't work
        //looks like Ability needs to be derived into PlayerAbility and MonsterAbility to account for different triggering and targeting :
        //a monster pre-computes it's targeting; player triggers it
    }
}
