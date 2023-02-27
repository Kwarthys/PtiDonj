using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyExplosionMovementEffectorEffect : Effect
{
    private GroundJumpEffector effector;
    private Vector3 explosionCenter;

    public ApplyExplosionMovementEffectorEffect(string effectName, GroundJumpEffector effector, Vector3 explosionCenter) : base(effectName, false)
    {
        this.effector = effector;
        this.explosionCenter = explosionCenter;
    }

    public override void onStart()
    {
        Vector3 pushDirection;
        if(owner.transform.position == explosionCenter)
        {
            pushDirection = new Vector3(Random.value, 0, Random.value);
        }
        else
        {
            pushDirection = (owner.transform.position - explosionCenter);
        }

        pushDirection.Normalize();

        if(AbilityTargetingData.tryFindGroundUnder(owner.transform.position + pushDirection * effector.jumpSpeed, out Vector3 groundHit))
        {
            AbilityTargetingData targeting = new AbilityTargetingData();
            targeting.charDidHit = false;
            targeting.groundDidHit = true;
            targeting.groundHit = groundHit;

            owner.registerNewMovementEffector(effector, targeting);
        }


    }
}
