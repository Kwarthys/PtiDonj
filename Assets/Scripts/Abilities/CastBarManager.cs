using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CastBarManager : NetworkBehaviour
{
    public CharacterStats selfStats { get; protected set; }

    private void Start()
    {
        selfStats = GetComponent<CharacterStats>();
    }


    public void setupCastBarAnimation(float castTime, string abilityName, CastBarDisplayController.FillMode fillMode, bool fromServer = false)
    {
        if(fromServer)
        {
            RpcSetupCastBar(castTime, abilityName, fillMode);
        }
        else
        {
            CmdSetupCastBar(castTime, abilityName, fillMode);
        }
    }

    public void interruptCastBarAnimation(bool fromServer = false)
    {
        if(fromServer)
        {
            RpcInterruptCastBarAnimation();
        }
        else
        {
            CmdInterruptCastBarAnimation();
        }
    }

    [Command(requiresAuthority = false)] //dunno why this is suddenly needed
    //[Command]
    protected void CmdSetupCastBar(float castTime, string abilityName, CastBarDisplayController.FillMode fillMode)
    {
        RpcSetupCastBar(castTime, abilityName, fillMode);
    }

    [ClientRpc]
    protected void RpcSetupCastBar(float castTime, string abilityName, CastBarDisplayController.FillMode fillMode)
    {
        selfStats.setupCastBarAnimation(castTime, abilityName, CastBarDisplayController.FillMode.FillUp);
    }

    [Command]
    protected void CmdInterruptCastBarAnimation()
    {
        RpcInterruptCastBarAnimation();
    }

    [ClientRpc]
    protected void RpcInterruptCastBarAnimation()
    {
        selfStats.interruptCastBarAnimation();
    }
}
