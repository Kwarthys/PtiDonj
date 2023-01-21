using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAbilityManager : MonoBehaviour
{
    public List<MonsterAbilityTrigger> abilityTriggers;

    private void Start()
    {
        MonsterAbilityTrigger[] triggers = GetComponentsInChildren<MonsterAbilityTrigger>();

        for (int i = 0; i < triggers.Length; i++)
        {
            if(triggers[i] != null)
            {
                abilityTriggers.Add(triggers[i]);
            }
        }
    }

    public void updateMonsterTriggers()
    {
        for (int i = 0; i < abilityTriggers.Count; i++)
        {
            if(abilityTriggers[i].isTriggerValid())
            {
                abilityTriggers[i].linkedAbility.tryCastAbility();
            }
        }
    }
}
