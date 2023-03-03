using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    protected Transform target;

    public void setTarget(Transform target)
    {
        this.target = target;
    }
}
