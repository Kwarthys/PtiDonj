using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ExplosionVFXController : VFXController
{
    public float suppressionDelay = 3f;
    private bool playedExplosion = false;

    public override void initialize(float size, float duration)
    {
        vfx.SetFloat("ExplosionSize", size);

        this.duration = duration;
        deltaTimeCounter = 0;
        playedExplosion = false;

        vfx.Stop();
    }

    public override bool updateAnimation()
    {
        deltaTimeCounter += Time.deltaTime;

        if(deltaTimeCounter > duration && !playedExplosion)
        {
            vfx.Play();
            playedExplosion = true;
        }

        return deltaTimeCounter < duration + suppressionDelay;
    }
}
