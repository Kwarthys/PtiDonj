using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToward : FollowTarget, IMyRigAnimator
{
    public Transform toMove;

    public float speed = 5;
    public float rotSpeed = 5;

    public float stopDistance = 3;

    // Update is called once per frame
    public void updateAnimator()
    {
        float step = speed * Time.deltaTime;

        float distance = Vector3.Distance(toMove.position, target.position);

        Vector3 cleanedTargetPos = target.position;
        cleanedTargetPos.y = 0;

        Vector3 cleanedPos = toMove.position;
        cleanedPos.y = 0;

        if(distance > step && distance > stopDistance)
        {
            toMove.position += step * (cleanedTargetPos - cleanedPos).normalized;

            toMove.rotation = Quaternion.RotateTowards(toMove.rotation, Quaternion.LookRotation(cleanedTargetPos - cleanedPos, Vector3.up), rotSpeed);
        }

    }
}
