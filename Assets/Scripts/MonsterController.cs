using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public float speed = 2f;

    private Transform target;

    public CharacterStats monsterStats;

    public MonsterAbilityManager abilityManager;

    public void updateMonster()
    {
        monsterStats.updateStats();

        if(target == null)
        {
            target = choseNewTarget();
        }

        if(target != null) //if chose new target failed
        {
            //move towards target
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            float step = speed * Time.deltaTime; //delta time may have to change, as it's computed only on the server

            if (step < distanceToTarget)
            {
                // please stop moving for debug
                //transform.position += (target.position - transform.position).normalized * step;
            }
        }
        

        if(Random.value > 0.995f)
        {
            target = null;

            //abilityManager.castAbility(0);
        }
    }

    private Transform choseNewTarget()
    {
        CharacterStats player = GameManager.instance.getRandomCharacter();
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
