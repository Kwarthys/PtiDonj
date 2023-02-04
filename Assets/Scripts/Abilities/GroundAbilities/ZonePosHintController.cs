using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonePosHintController : MonoBehaviour
{
    private Material zoneMaterial;

    public void setPlayerInside(bool state)
    {
        zoneMaterial.SetFloat("_Opacity", state ? 1 : 0);
    }

    public void initialize()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        zoneMaterial = new Material(renderer.material);
        renderer.material = zoneMaterial;
    }
}
