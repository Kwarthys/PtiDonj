using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootIKAnimator : MonoBehaviour
{
    public Transform body;
    public float length;
    public float angle;
    public float stepDistance = 5;
    private float stepDistanceCoef = 1;

    public float legsSpeed = 5;
    public float stepHeight = 1;

    public int animIndex = 0;

    private float lerp = 1;
    private Vector3 newPos;
    private Vector3 lastPos;
    private bool lastPosInitialized = false;

    public void halfNextStep()
    {
        stepDistanceCoef = 0.5f; //half step size only of next step
    }

    public bool onGround()
    {
        return lerp >= 1;
    }

    private void Update()
    {
        Vector3 offset = Quaternion.Euler(0, angle, 0) * body.forward;
        offset = offset.normalized * length;

        Ray ray = new Ray(body.position + offset, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, 50, LocalReferencer.instance.groundLayer))
        {
            if(!lastPosInitialized)
            {
                transform.position = hit.point;
                lastPos = hit.point;
                newPos = hit.point;

                lastPosInitialized = true;
            }
            else
            {
                if (Vector3.Distance(newPos, hit.point) > stepDistance * stepDistanceCoef)
                {
                    newPos = transform.position + 1.8f * stepDistance * stepDistanceCoef * (hit.point - transform.position).normalized;
                    lastPos = transform.position;
                    lerp = 0;
                    stepDistanceCoef = 1; //back to full steps
                }
                else
                {
                    transform.position = lastPos;
                }
            }            
        }

        if(lerp < 1)
        {
            lerp += Time.deltaTime * legsSpeed;

            if(lerp >= 1)
            {
                lastPos = newPos;
                transform.position = lastPos;
            }
            else
            {
                Vector3 pos = Vector3.Lerp(lastPos, newPos, lerp);
                pos.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;

                transform.position = pos;
            }
        }
    }
}
