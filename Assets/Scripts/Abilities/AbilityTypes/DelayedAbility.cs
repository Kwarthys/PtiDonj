using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public override bool canCastAbility()
    {
        return canCast && !ownerStats.moving;
    }

    public override void onAbilityUpdate()
    {
        if(casting)
        {
            delayDeltaTimeCounter += Time.deltaTime;

            if(delayDeltaTimeCounter > castTime)
            {
                computeTargetingAndApplyAbility();
                onCast();
                casting = false;
            }

            if (ownerStats.moving)
            {
                casting = false; //Interrupted
                ownerStats.interruptCastBarAnimation();
                ErrorMessageController.instance.showText("Cast interrupted, stay still !");
            }

            if (!casting)
            {
                cooldownDeltaTimeCounter = 0; //starts cd on interrupt or on cast
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
}
