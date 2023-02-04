using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ExplosionVFXController : MonoBehaviour, IMyAnimator
{
    public VisualEffect vfx;

    private float duration = 3;
    private float delay = 0;

    private float deltaTimeCounter = 0;
    private bool playedExplosion = false;

    public void setupExplosion(float size, float delay, float duration)
    {
        vfx.SetFloat("ExplosionSize", size);

        this.delay = delay;
        this.duration = duration;
        deltaTimeCounter = 0;
        playedExplosion = false;

        vfx.Stop();
    }

    public bool updateAnimation()
    {
        deltaTimeCounter += Time.deltaTime;

        if(deltaTimeCounter > delay && !playedExplosion)
        {
            vfx.Play();
            playedExplosion = true;
        }

        return deltaTimeCounter < duration + delay;
    }

    public void destroyAnimator()
    {
        Destroy(gameObject);
    }
}
