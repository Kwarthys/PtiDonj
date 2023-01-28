using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CastBarDisplayController : MonoBehaviour
{
    public enum FillMode { FillUp, Empty};

    public Image image;

    public TextMeshProUGUI textMesh;

    private float animationDuration = 0;
    private float deltaTimeCounter = 0;

    private float interruptedAnimationDuration = 1;
    private float interruptionDeltaTimeCounter = 0;
    private bool interrupted = false;

    private FillMode fillMode;
    public bool needsUpdate { get; private set; } = false;

    public void updateAnimation()
    {
        if (!needsUpdate) return;

        bool ended = false;

        if(!interrupted)
        {
            deltaTimeCounter += Time.deltaTime;
            float t = deltaTimeCounter / animationDuration;

            if (fillMode == FillMode.Empty)
            {
                t = 1 - t;
            }

            image.material.SetFloat("_LifePercent", t);
            if (fillMode == FillMode.FillUp)
            {
                ended = t > 1;
            }
            else if (fillMode == FillMode.Empty)
            {
                ended = t < 0;
            }
        }
        else
        {
            interruptionDeltaTimeCounter += Time.deltaTime;
            if(interruptionDeltaTimeCounter > interruptedAnimationDuration)
            {
                ended = true;
            }
        }

        if (ended)
        {
            needsUpdate = false;

            textMesh.gameObject.SetActive(false);
            image.gameObject.SetActive(false);
        }


    }

    public void setupAnimationAndPlay(float animationDuration, string text, FillMode fillMode = FillMode.FillUp)
    {
        this.animationDuration = animationDuration;
        this.fillMode = fillMode;
        textMesh.gameObject.SetActive(true);
        image.gameObject.SetActive(true);
        textMesh.text = text;

        deltaTimeCounter = 0;
        interruptionDeltaTimeCounter = 0;
        interrupted = false;

        needsUpdate = true;
    }

    public void interruptCastBar()
    {
        //red flashy stuff, then out
        if(needsUpdate)
        {
            //don't interrupt if not animating
            interrupted = true;
            textMesh.text = "Interrupted";
        }
    }

    private void Awake()
    {
        image.material = new Material(image.material);
    }

    private void Start()
    {
        textMesh.gameObject.SetActive(false);
        image.gameObject.SetActive(false);
    }
}
