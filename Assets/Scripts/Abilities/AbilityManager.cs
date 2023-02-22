using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class AbilityManager : NetworkBehaviour
{
    public Ability basicAbility;
    public Ability secondBasicAbility;

    private Ability[] abilities;

    public Transform visor;

    public CharacterStats selfStats;

    public LayerMask groundLayer;

    private List<AbilityWidgetController> widgets = new List<AbilityWidgetController>();

    private void Awake()
    {
        abilities = new Ability[2];

        basicAbility.abilityIndex = 0;
        abilities[0] = basicAbility;

        secondBasicAbility.abilityIndex = 1; //this will be automated by loadout setup by player (when implmented)
        abilities[1] = secondBasicAbility;
    }

    public void setupLocalPlayerAbilityDisplay()
    {
        GameObject widgetPrefab = LocalReferencer.instance.abilityWidgetPrefab;
        Transform holder = LocalReferencer.instance.abilityWidgetsHolder;

        AbilityWidgetController w;

        w = Instantiate(widgetPrefab, holder).GetComponent<AbilityWidgetController>();
        w.setup(basicAbility);
        widgets.Add(w);

        w = Instantiate(widgetPrefab, holder).GetComponent<AbilityWidgetController>();
        w.setup(secondBasicAbility);
        widgets.Add(w);
    }

    public void castBasicAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            CmdCastBasicAbility(selfStats.netIdentity);
        }
    }

    [Command]
    public void CmdCastBasicAbility(NetworkIdentity caster)
    {
        if (basicAbility.canCastAbility())
        {
            bool fired = basicAbility.tryCastAbility();

            if (fired)
            {
                TargetRpcNotifyAbilityFired(caster.connectionToClient, 0);
            }
        }
    }

    public void castSecondBasicAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            CmdCastSecondBasicAbility(selfStats.netIdentity);
        }
    }

    [Command]
    public void CmdCastSecondBasicAbility(NetworkIdentity caster)
    {
        if (secondBasicAbility.canCastAbility())
        {
            bool fired = secondBasicAbility.tryCastAbility();

            if (fired)
            {
                TargetRpcNotifyAbilityFired(caster.connectionToClient, 1); //Client abilities
            }
        }
    }

    public void notifyAbilityFired(int abilityIndex)
    {
        Ability fired;

        if (abilityIndex == 0)
        {
            fired = basicAbility;
        }
        else
        {
            fired = secondBasicAbility;
        }

        fired.associatedWidget.abilityFired();
        fired.notifyAbilityFired();
    }

    [TargetRpc]
    public void TargetRpcNotifyAbilityFired(NetworkConnection target, int abilityIndex)
    {
        notifyAbilityFired(abilityIndex);
    }

    [Command(requiresAuthority = false)] //dunno why this is suddenly needed
    //[Command]
    public void CmdSetupCastBar(float castTime, string abilityName, CastBarDisplayController.FillMode fillMode)
    {
        RpcSetupCastBar(castTime, abilityName, fillMode);
    }

    [ClientRpc]
    public void RpcSetupCastBar(float castTime, string abilityName, CastBarDisplayController.FillMode fillMode)
    {
        selfStats.setupCastBarAnimation(castTime, abilityName, CastBarDisplayController.FillMode.FillUp);
    }

    [Command]
    public void CmdInterruptCastBarAnimation()
    {
        RpcInterruptCastBarAnimation();
    }

    [ClientRpc]
    public void RpcInterruptCastBarAnimation()
    {
        selfStats.interruptCastBarAnimation();
    }

    [Command]
    public void CmdNotifyAbilityInterruption(int abilityIndex)
    {
        RpcNotifyAbilitInterruption(abilityIndex);
    }

    [ClientRpc]
    public void RpcNotifyAbilitInterruption(int abilityIndex)
    {
        if(abilities[abilityIndex] is DelayedAbility ability)
        {
            ability.interruptAbility();
        }
    }


    public void updateAbilities(bool animationOnly)
    {
        if(basicAbility.needsUpdate())
        {
            basicAbility.onAbilityUpdate(animationOnly);
        }        

        if (secondBasicAbility.needsUpdate())
        {
            secondBasicAbility.onAbilityUpdate(animationOnly);
        }

        for (int i = 0; i < widgets.Count; i++)
        {
            if(widgets[i].needsUpdate)
            {
                widgets[i].updateAnimation();
            }
        }
    }
}
