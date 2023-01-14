using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyEffectWhileInColliderEffect : TickingEffect
{
    public Collider areaOfEffect;

    public EffectDescriptor[] effectsToApply;

    public LayerMask targetsLayer;

    public ColliderTriggerHandler colliderTriggers;

    public override void onTick()
    {
        List<CharacterStats> targetsInside = colliderTriggers.getTargetsInside(targetsLayer);

        for (int j = 0; j < targetsInside.Count; j++)
        {
            applyEffectsTo(targetsInside[j], effectsToApply);
        }
    }
}
