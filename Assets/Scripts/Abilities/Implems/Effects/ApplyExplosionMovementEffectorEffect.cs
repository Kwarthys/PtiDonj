using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyExplosionMovementEffectorEffect : Effect
{
    private Vector3 explosionCenter;

    public float jumpLength;
    public float jumpHeight;

    public ApplyExplosionMovementEffectorEffect(string effectName, Vector3 explosionCenter, float jumpLength, float jumpHeight) : base(effectName, false)
    {
        this.explosionCenter = explosionCenter;

        this.jumpHeight = jumpHeight;
        this.jumpLength = jumpLength;
    }

    public override void onStart()
    {
        owner.TargetRPCSetupExplosionEffector(owner.netIdentity.connectionToClient, false, 30, jumpHeight, explosionCenter, jumpLength);
    }
}
