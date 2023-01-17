using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IColliderEffect
{
    public void registerColliderTrigger(ColliderTriggerHandler triggerHandler);
    public float getZoneSize();

}
