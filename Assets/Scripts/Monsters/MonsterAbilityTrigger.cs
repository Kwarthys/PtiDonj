using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterAbilityTrigger : MonoBehaviour
{
    public Ability linkedAbility { get; private set; }
    void Start()
    {
        linkedAbility = GetComponent<Ability>();
    }

    public abstract bool isTriggerValid();
}
