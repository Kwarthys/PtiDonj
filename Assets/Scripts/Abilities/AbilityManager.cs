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
        if(basicAbility.canCastAbility())
        {
            basicAbility.tryCastAbility();
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
        if(secondBasicAbility.canCastAbility())
        {
            secondBasicAbility.tryCastAbility();
        }
    }

    public void updateAbilities()
    {
        if(basicAbility.needsUpdate())
        {
            basicAbility.onAbilityUpdate();
        }        

        if (secondBasicAbility.needsUpdate())
        {
            secondBasicAbility.onAbilityUpdate();
        }
    }
}
