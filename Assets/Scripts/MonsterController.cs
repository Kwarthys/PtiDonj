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
            Debug.Log("Found new target : " + target.name);
        }

        //move towards target
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        float step = speed * Time.deltaTime; //delta time may have to change, as it's computed only on the server

        if(step < distanceToTarget)
        {
            transform.position += (target.position - transform.position).normalized * step;
        }

        if(Random.value > 0.995f)
        {
            target = null;

            abilityManager.castAbilityOnRandomPlayer(0);
        }
    }

    private Transform choseNewTarget()
    {
        return GameManager.instance.getRandomCharacter().transform;
    }
}
