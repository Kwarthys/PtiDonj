using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LifeDisplayController : MonoBehaviour
{
    public Image image;

    public TextMeshProUGUI healthText;

    public void updateLifeDisplay(float lifePercent, float lifeReal)
    {
        lifePercent = Mathf.Clamp(lifePercent, 0f, 1f);
        image.material.SetFloat("_LifePercent", lifePercent);
        healthText.text = lifeReal.ToString();
    }
}
