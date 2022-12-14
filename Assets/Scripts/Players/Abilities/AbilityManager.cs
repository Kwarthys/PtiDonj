using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityManager : MonoBehaviour
{
    public Ability basicAbility;

    public Transform visor;

    public CharacterStats selfStats;

    public void castBasicAttack(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            basicAbility.tryCastAbility();
            Debug.Log("Cast");
        }
    }

    public AbilityTargetingResult tryGetTarget(LayerMask mask)
    {
        AbilityTargetingResult result = new AbilityTargetingResult();

        result.didHit = false;

        Debug.DrawRay(visor.position, visor.forward * 10, Color.black, 3);

        if (Physics.Raycast(visor.position, visor.forward, out RaycastHit hit, 250, mask))
        {
            result.didHit = true;
            result.pointHit = hit.point;
            result.charHit = hit.transform.GetComponent<CharacterStats>();
        }

        return result;
    }
}

public class AbilityTargetingResult
{
    public bool didHit;
    public Vector3 pointHit;
    public CharacterStats charHit = null;
}
