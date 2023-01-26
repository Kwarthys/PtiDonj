using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextMeshProHandler : MonoBehaviour
{
    [SerializeField]
    protected TMP_Text textMesh;

    protected void setText(string text)
    {
        textMesh.text = text;
    }

    protected void setSize(float size)
    {
        textMesh.fontSize = size;
    }
}
