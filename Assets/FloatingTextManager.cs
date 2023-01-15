using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextManager : MonoBehaviour
{
    public static FloatingTextManager instance;
    private void Awake()
    {
        instance = this;
    }

    public GameObject floatingTextPrefab;
    public Transform floatingTextHolder;

    public float maxTextSize = 50;

    public AnimationCurve sizeOverAnimation;

    public void spawnFloatingText(Vector3 pos, string text)
    {
        Transform localPlayerTransform = GameManager.instance.localPlayerTransform;

        Vector2 randomVector = Random.insideUnitCircle.normalized;
        Vector3 randomVector3D = new Vector3(randomVector.x, Mathf.Abs(randomVector.y), 0);
        randomVector3D = localPlayerTransform.localToWorldMatrix * randomVector3D;

        float textToPlayerSqrDistance = (localPlayerTransform.position - pos).magnitude;
        Vector3 textToPlayerDirection = (localPlayerTransform.position - pos).normalized;

        float spawnOffset = textToPlayerSqrDistance / 10f;

        Vector3 spawnPos = pos + textToPlayerDirection * 2.0f;
        spawnPos += new Vector3(0, spawnOffset / 2f, 0);
        spawnPos += randomVector3D * spawnOffset;

        GameObject go = Instantiate(floatingTextPrefab, spawnPos, Quaternion.identity, floatingTextHolder);

        FloatingTextController controller = go.GetComponent<FloatingTextController>();
        controller.setText(text);

        FloatingTextAnimator animator = go.GetComponent<FloatingTextAnimator>();
        animator.setupAnimator(localPlayerTransform, Mathf.Min(maxTextSize, textToPlayerSqrDistance));
        animator.animationDirection = randomVector3D;
        GameManager.instance.registerAnimatedObject(animator);
    }
}
