using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DelayedAbility : Ability
{
    public float castTime = 2f;
    protected float delayDeltaTimeCounter = 0;
    protected bool casting = false;

    public override bool tryCastAbility()
    {
        if(canCast && !casting)
        {
            casting = true;
            canCast = false;
            delayDeltaTimeCounter = 0;
            cooldownDeltaTimeCounter = 0;

            manager.CmdSetupCastBar(castTime, abilityName, CastBarDisplayController.FillMode.FillUp);
        }

        return casting;
    }

    public override void notifyAbilityFired()
    {
        base.notifyAbilityFired();
        casting = true;
    }

    public override bool canCastAbility()
    {
        return canCast && !ownerStats.moving;
    }

    public override void onAbilityUpdate(bool animationOnly)
    {
        if(casting)
        {
            delayDeltaTimeCounter += Time.deltaTime;

            if(delayDeltaTimeCounter > castTime)
            {
                if(!animationOnly)
                {
                    //this will not be called on clients
                    computeTargetingAndApplyAbility();
                }
                casting = false;
            }

            if (ownerStats.moving)
            {
                manager.CmdNotifyAbilityInterruption(abilityIndex);
                casting = false; //Interrupted
            }

            if(!casting)
            {
                delayDeltaTimeCounter = 0;
                cooldownDeltaTimeCounter = 0;
            }
        }
        else if(!canCast)
        {
            cooldownDeltaTimeCounter += Time.deltaTime;
            if(cooldownDeltaTimeCounter > cooldown)
            {
                canCast = true; //cd Refreshed
            }
        }
    }

    public void interruptAbility()
    {
        ErrorMessageController.instance.showText("Cast interrupted, stay still !");
        manager.CmdInterruptCastBarAnimation();
    }

    public override AbilityCooldownData getCooldownData()
    {
        AbilityCooldownData data = new AbilityCooldownData();

        if(canCast)
        {
            data.state = CooldownState.ready;
        }
        else
        {
            if(casting)
            {
                data.state = CooldownState.casting;
            }
            else
            {
                data.state = CooldownState.charging;
                data.fullCooldown = cooldown;
                data.cooldownSpent = cooldownDeltaTimeCounter;
            }
        }

        return data;
    }
}
