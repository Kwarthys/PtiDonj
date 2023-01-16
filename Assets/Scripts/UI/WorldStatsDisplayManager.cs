using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldStatsDisplayManager : StatsDisplayManager
{
    private void Awake()
    {
        needsOrientationUpdate = true;
    }

    protected override void updateDisplayOrientation()
    {
        if(GameManager.instance.localPlayerTransform != null)
        {
            transform.rotation = Quaternion.LookRotation(transform.position - GameManager.instance.localPlayerTransform.position);
        }
    }
}
