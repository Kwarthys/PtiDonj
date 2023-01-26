using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ErrorMessageController : TextMeshProHandler
{
    public static ErrorMessageController instance;

    public float textSize = 35f;
    public float timeBeforeFade = 2f;
    public float fadeTime = 1f;
    private bool animating = false;
    private float deltaTimeCounter = 0;

    private void Awake()
    {
        instance = this;
        textMesh.gameObject.SetActive(false);
    }

    public void updateAnimation()
    {
        if (!animating) return;

        deltaTimeCounter += Time.deltaTime;

        if(deltaTimeCounter > timeBeforeFade)
        {
            float fadeAmount = (deltaTimeCounter-timeBeforeFade) / fadeTime;    //0: no fade, 1:full faded

            if(fadeAmount > 1)
            {
                setSize(0);
                textMesh.gameObject.SetActive(false);
            }
            else
            {
                float fontSize = (1 - fadeAmount) * textSize;
                setSize(fontSize);
            }
        }
    }

    public void showText(string text)
    {
        setupAnimation(text);
    }

    private void setupAnimation(string text)
    {
        textMesh.gameObject.SetActive(true);
        setText(text);
        setSize(textSize);
        deltaTimeCounter = 0;
        animating = true;
    }

    public bool needsAnimationUpdate()
    {
        return animating;
    }
}
