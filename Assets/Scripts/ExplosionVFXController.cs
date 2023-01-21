using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ExplosionVFXController : MonoBehaviour
{
    public VisualEffect vfx;

    private void Start()
    {
        vfx.Play();
        Destroy(gameObject, 5); 
    }

    public void setExplosionSize(float size)
    {
        vfx.SetFloat("ExplosionSize", size);
    }
}
