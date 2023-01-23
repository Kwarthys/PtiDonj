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
        if(PlayerManager.instance.localPlayerTransform != null)
        {
            transform.rotation = Quaternion.LookRotation(transform.position - PlayerManager.instance.localPlayerTransform.position);
        }
    }
}
