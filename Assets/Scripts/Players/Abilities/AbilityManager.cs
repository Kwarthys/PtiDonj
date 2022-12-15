using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class AbilityManager : NetworkBehaviour
{
    public Ability basicAbility;

    public Transform visor;

    public CharacterStats selfStats;

    public void castBasicAttack(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            //basicAbility.tryCastAbility();
            CmdCastBasicAbility();
            //Debug.Log("Cast");
        }
    }

    [Command]
    public void CmdCastBasicAbility()
    {
        RpcCastBasicAbility();
    }

    [ClientRpc]
    public void RpcCastBasicAbility()
    {
        basicAbility.tryCastAbility();
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
}

public class AbilityTargetingResult
{
    public bool didHit;
    public Vector3 pointHit;
    public CharacterStats charHit = null;
}
