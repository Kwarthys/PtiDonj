using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastedDelayedAbility : RaycastedAbility
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
                AbilityTargetingData targeting = computeTargeting();
                applyAbility(targeting);
                onCast();
                casting = false;
            }

            if (ownerStats.moving)
            {
                casting = false; //Interrupted
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
