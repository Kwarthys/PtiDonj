using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAbilityManager : MonoBehaviour
{
    private List<MonsterAbilityTrigger> abilityTriggers = new List<MonsterAbilityTrigger>();

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

    public void updateMonsterAbilities()
    {
        for (int i = 0; i < abilityTriggers.Count; i++)
        {
            Ability a = abilityTriggers[i].linkedAbility;

            if(a.needsUpdate())
            {
                a.onAbilityUpdate(false);
            }

            if (abilityTriggers[i].isTriggerValid())
            {
                a.tryCastAbility();
            }
        }
    }
}
