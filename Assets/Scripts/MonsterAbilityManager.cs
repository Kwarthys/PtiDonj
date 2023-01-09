using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAbilityManager : MonoBehaviour
{
    public List<Ability> abilities;

    public LayerMask groundLayer;

    public void castAbilityOnRandomPlayer(int abilityIndex)
    {
        if(abilities[abilityIndex].canCast())
        {
            AbilityTargetingResult targeting = targetPlayer(GameManager.instance.getRandomCharacter());

            if(targeting.didHit)
            {
                abilities[abilityIndex].tryCastAbility(targeting);
            }
        }
    }

    private AbilityTargetingResult targetPlayer(CharacterStats player)
    {
        AbilityTargetingResult targeting = new AbilityTargetingResult();

        if(player == null)
        {
            targeting.didHit = false;
            return targeting;
        }

        targeting.didHit = true;
        targeting.charHit = player;

        if (Physics.Raycast(player.transform.position + Vector3.up, Vector3.down, out RaycastHit floorhit, 100, groundLayer)) //looking for floor
        {
            targeting.pointHit = floorhit.point;
        }
        else
        {
            targeting.didHit = false;
        }

        return targeting;
    }
}
