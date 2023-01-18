using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfStatsDisplayManager : StatsDisplayManager
{
    private void Awake()
    {
        needsOrientationUpdate = false;
    }
}
