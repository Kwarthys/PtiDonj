using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyExplosionMovementEffectorEffectDescriptor : EffectDescriptor
{
    private GroundJumpEffector effector;

    public float jumpLength;
    public float jumpHeigth;

    private void Start()
    {
        effector = gameObject.AddComponent<GroundJumpEffector>();

        effector.speedIsJumpDuration = true;
        effector.jumpSpeed = jumpLength;

        effector.jumpHeigth = jumpHeigth;
    }

    public override Effect getNewEffect()
    {
        ApplyExplosionMovementEffectorEffect effect = new ApplyExplosionMovementEffectorEffect(effectName, effector, transform.position);

        return effect;
    }
}
