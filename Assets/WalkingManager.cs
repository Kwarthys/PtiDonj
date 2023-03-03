using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingManager : MonoBehaviour
{
    private FootIKAnimator[] feet;

    private FollowTarget[] followers;

    public Transform target;

    public void setTarget(Transform target)
    {
        this.target = target;

        for (int i = 0; i < followers.Length; i++)
        {
            followers[i].setTarget(target);
        }
    }

    void Start()
    {
        followers = gameObject.GetComponentsInChildren<FollowTarget>();

        feet = gameObject.GetComponentsInChildren<FootIKAnimator>();

        for (int i = 0; i < feet.Length; i++)
        {
            if(feet[i].animIndex == 0)
            {
                feet[i].halfNextStep();
            }
        }

        setTarget(target);
    }
}
