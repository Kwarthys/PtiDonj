using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public abstract class VFXController : MonoBehaviour, IMyAnimator
{
    protected float deltaTimeCounter = 0;
    protected float duration = 3;
    public VisualEffect vfx;

    public abstract void initialize(float size, float duration);


    public void destroyAnimator()
    {
        Destroy(gameObject);
    }

    public abstract bool updateAnimation();
}
