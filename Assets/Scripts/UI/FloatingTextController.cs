using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingTextController : MonoBehaviour
{
    public TextMeshPro textMesh;

    public void setText(string text)
    {
        textMesh.text = text;
    }

    public void setSize(float size)
    {
        textMesh.fontSize = size;
    }
}
