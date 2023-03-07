using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private BossAnimatorManager animator;

    private Transform target;

    public CharacterStats monsterStats;

    public MonsterAbilityManager abilityManager;

    public bool castAbilitiesDebug = true;

    private void Start()
    {
        animator = gameObject.GetComponentInChildren<BossAnimatorManager>();
    }

    public void updateMonster()
    {
        monsterStats.updateStats();

        if(target == null)
        {
            target = choseNewTarget();
        }
        

        if(Random.value > 0.995f)
        {
            target = null;
        }

        if (castAbilitiesDebug)
        {
            abilityManager.updateMonsterAbilities();
        }
    }

    public void updateAnimator()
    {
        animator?.updateAnimator();
    }

    private Transform choseNewTarget()
    {
        CharacterStats player = PlayerManager.instance.getRandomCharacter();
        if(player == null)
        {
            return null;
        }
        else
        {
            return player.transform;
        }
    }
}
