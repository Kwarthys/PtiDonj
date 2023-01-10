using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAbilityManager : MonoBehaviour
{
    public List<Ability> abilities;

    public void castAbility(int abilityIndex)
    {
        if(abilities[abilityIndex].canCast())
        {            
            abilities[abilityIndex].tryCastAbility();
        }
    }
}
