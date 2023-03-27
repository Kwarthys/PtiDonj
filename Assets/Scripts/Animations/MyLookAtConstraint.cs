using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLookAtConstraint : FollowTarget, IMyRigAnimator
{
    public Transform toRotate;

    public bool lockX = false;
    public bool lockY = false;
    public bool lockZ = false;

    public void updateAnimator()
    {
        Quaternion lookAt = Quaternion.LookRotation(toRotate.position - target.position, Vector3.up);

        Vector3 eulerLookAt = lookAt.eulerAngles;

        if (lockX)
        {
            eulerLookAt.x = toRotate.eulerAngles.x;
        }

        if (lockY)
        {
            eulerLookAt.y = toRotate.eulerAngles.y;
        }

        if (lockZ)
        {
            eulerLookAt.z = toRotate.eulerAngles.z;
        }

        toRotate.rotation = Quaternion.Euler(eulerLookAt);
    }
}
