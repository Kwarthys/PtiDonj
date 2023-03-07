using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyHeightHandler : MonoBehaviour
{
    public Transform toMove;
    public Transform[] anchors;
    public float heightOffset = 5;

    public float speed = 1;

    public bool running = true;

    public void Update()
    {
        if (!running) return; //this will be tested pre-update when centralized

        float meanAnchors = 0;
        for (int i = 0; i < anchors.Length; i++)
        {
            meanAnchors += anchors[i].position.y;
        }
        meanAnchors /= anchors.Length;

        float targetY = meanAnchors + heightOffset;
        float y = toMove.position.y;

        if(targetY - y != 0)
        {
            float stepY = (targetY - y) * Time.deltaTime * speed;

            Vector3 newPos = toMove.position;
            newPos.y += stepY;

            toMove.transform.position = newPos;
        }
    }
}
