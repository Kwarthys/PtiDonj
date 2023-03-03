using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToward : FollowTarget
{
    public Transform toMove;

    public float speed = 5;
    public float rotSpeed = 5;

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;

        float distance = Vector3.Distance(toMove.position, target.position);

        Vector3 cleanedTargetPos = target.position;
        cleanedTargetPos.y = 0;

        Vector3 cleanedPos = toMove.position;
        cleanedPos.y = 0;

        if(distance > step)
        {
            toMove.position += step * (cleanedTargetPos - cleanedPos).normalized;

            toMove.rotation = Quaternion.RotateTowards(toMove.rotation, Quaternion.LookRotation(cleanedTargetPos - cleanedPos, Vector3.up), rotSpeed);
        }

    }
}
