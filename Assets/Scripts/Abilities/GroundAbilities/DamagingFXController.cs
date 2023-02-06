using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class DamagingFXController : MonoBehaviour, IMyAnimator
{
    public VisualEffect vfx;

    private float duration = 3;
    public float fadeOutTime = 1f;
    public float delay = 1f;

    private float deltaTimeCounter = 0;

    private bool faded = false;

    public void initialize(float size, float duration)
    {
        vfx.SetFloat("Size", size);
        vfx.SetFloat("SpawnRate", 1);

        this.duration = duration;
        faded = false;

        if(fadeOutTime > duration)
        {
            fadeOutTime = duration;
        }
    }

    public void destroyAnimator()
    {
        Destroy(gameObject);
    }

    public bool updateAnimation()
    {
        deltaTimeCounter += Time.deltaTime;

        if(deltaTimeCounter > duration - fadeOutTime && !faded)
        {
            float fadeOutAmount = (deltaTimeCounter - duration + fadeOutTime) / fadeOutTime;

            fadeOutAmount = 1 - fadeOutAmount;

            if(fadeOutAmount < 0)
            {
                fadeOutAmount = 0;
                faded = true;
            }

            vfx.SetFloat("SpawnRate", fadeOutAmount);
        }

        return deltaTimeCounter < duration + delay;
    }
}
