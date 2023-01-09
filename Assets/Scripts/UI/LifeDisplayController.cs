using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LifeDisplayController : MonoBehaviour
{
    public Image image;

    public TextMeshProUGUI healthText;

    public float animationTime = 0.5f;
    private float startedAnimation = -1;
    private float animationRelativeLifeStart;
    private float animationRelativeLifeTarget;
    private float animationAbsoluteLifeStart;
    private float animationAbsoluteLifeTarget;

    private float currentLifePercent = 0;
    private float currentLifeReal = 0;

    public bool needsUpdate { get; private set; } = false;

    public void updateLifeDisplay(float lifePercent, float lifeReal, bool bypassAnimation = false)
    {
        if(bypassAnimation)
        {
            setDisplay(lifePercent, lifeReal);
        }
        else
        {
            prepareAnimation(lifePercent, lifeReal);
        }

        currentLifePercent = lifePercent;
        currentLifeReal = lifeReal;
    }

    private void prepareAnimation(float lifePercent, float lifeReal)
    {
        if(!needsUpdate) //We don't update sarting point if already animating
        {
            animationRelativeLifeStart = currentLifePercent;
            animationAbsoluteLifeStart = currentLifeReal;

            startedAnimation = Time.realtimeSinceStartup;
        }

        animationRelativeLifeTarget = lifePercent;
        animationAbsoluteLifeTarget = lifeReal;

        needsUpdate = true;

        Debug.Log("From " + animationRelativeLifeStart + " to " + animationRelativeLifeTarget);
    }

    public void updateDisplayAnimation()
    {
        float t = (Time.realtimeSinceStartup - startedAnimation) / animationTime;

        if(t>1)
        {
            t = 1;
            needsUpdate = false;
        }

        float relativeLifeLerp = Mathf.Lerp(animationRelativeLifeStart, animationRelativeLifeTarget, t);
        float absoluteLifeLerp = Mathf.Lerp(animationAbsoluteLifeStart, animationAbsoluteLifeTarget, t);

        setDisplay(relativeLifeLerp, absoluteLifeLerp);
    }

    private void setDisplay(float relative, float absolute)
    {
        relative = Mathf.Clamp(relative, 0f, 1f);
        image.material.SetFloat("_LifePercent", relative);

        healthText.text = Mathf.RoundToInt(absolute).ToString();
    }
}
    