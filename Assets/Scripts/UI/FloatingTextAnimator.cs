using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextAnimator : MonoBehaviour, IMyAnimator
{
    public float animationTime = 0.3f;
    private float animationStart = -1;

    public float animationMovementSpeed = 1;

    public float animationCompletion = 0;

    public float animationTextGravity = 0.01f;

    public Vector3 animationDirection = new Vector3(0, 1, 0);

    public bool animate = true;

    private Transform toLookAt;

    private float textDesiredSize = 10;

    public FloatingTextController controller;

    public void setupAnimator(Transform toLookAt, float textDesiredSize)
    {
        this.toLookAt = toLookAt;
        animationStart = Time.realtimeSinceStartup;
        this.textDesiredSize = textDesiredSize;
    }

    public bool updateAnimation()
    {
        if (!animate) return false;

        transform.position += animationDirection * animationMovementSpeed * Time.deltaTime;

        animationCompletion = (Time.realtimeSinceStartup - animationStart) / animationTime;

        transform.rotation = Quaternion.LookRotation(toLookAt.position - transform.position);

        animationDirection.y -= animationTextGravity;

        controller.setSize(textDesiredSize * FloatingTextManager.instance.sizeOverAnimation.Evaluate(animationCompletion));

        if (animationCompletion > 1)
        {
            animate = false;
            return false;
        }

        return true;
    }

    public void destroyAnimator()
    {
        Destroy(gameObject);
    }
}
