using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class AbilityManager : NetworkBehaviour
{
    public Ability basicAbility;
    public Ability secondBasicAbility;

    public Transform visor;

    public CharacterStats selfStats;

    public LayerMask groundLayer;

    public void castBasicAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            CmdCastBasicAbility();
        }
    }

    [Command]
    public void CmdCastBasicAbility()
    {
        if(basicAbility.canCast())
        {
            AbilityTargetingResult targeting = tryGetTarget(basicAbility.targetLayer);
            basicAbility.tryCastAbility(targeting);
        }
    }

    public void castSecondBasicAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            CmdCastSecondBasicAbility();
        }
    }

    [Command]
    public void CmdCastSecondBasicAbility()
    {
        if(secondBasicAbility.canCast())
        {
            AbilityTargetingResult targeting = tryGetTarget(secondBasicAbility.targetLayer);
            secondBasicAbility.tryCastAbility(targeting);
        }
    }

    public AbilityTargetingResult tryGetTarget(LayerMask mask)
    {
        AbilityTargetingResult result = new AbilityTargetingResult();

        result.didHit = false;

        Debug.DrawRay(visor.position, visor.forward * 10, Color.black, 3);

        if (Physics.Raycast(visor.position, visor.forward, out RaycastHit hit, 250, mask | groundLayer))
        {
            result.didHit = true;
            result.charHit = hit.transform.GetComponent<CharacterStats>();
            result.pointHit = new Vector3(0, 0, 0); //this should always be updated, if didHit is true

            drawDebugCrossAtPoint(hit.point);

            if (Physics.Raycast(hit.point + Vector3.up, Vector3.down, out RaycastHit floorhit, 100, groundLayer)) //looking for floor
            {
                result.pointHit = floorhit.point;
            }
            else
            {
                Debug.LogWarning("Did not find floor"); //not sure if this case will ever occur, tracking it just in case
            }

            //drawDebugCrossAtPoint(result.pointHit);
        }

        return result;
    }

    /*
    public void askApplyEffectTo(CharacterStats target)//, Effect effect)
    {
        CmdApplyEffectsTo(target.networkIdentity.netId);//, effect);
    }

    [Command]
    public void CmdApplyEffectsTo(uint netId)//, Effect effect)
    {
        //GameManager.instance.getCharacter(netId).addEffect(effect);
        Debug.Log("Added effect to " + netId);
    }
    */


    /***
     * DEBUG 
     */
    private void drawDebugCrossAtPoint(Vector3 worldPoint)
    {
        Debug.DrawLine(worldPoint + Vector3.left, worldPoint + Vector3.right * 2, Color.red, 5);
        Debug.DrawLine(worldPoint + Vector3.forward, worldPoint + Vector3.back * 2, Color.blue, 5);
        Debug.DrawLine(worldPoint + Vector3.up, worldPoint + Vector3.down * 2, Color.yellow, 5);
    }
}

public class AbilityTargetingResult
{
    public bool didHit;
    public Vector3 pointHit;
    public CharacterStats charHit = null;
}
