using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyEffectOnColliderEffect : TickingEffect
{
    public Collider areaOfEffect;

    public EffectDescriptor[] effectsToApply;

    public LayerMask targetsLayer;

    public ColliderTriggerHandler colliderTriggers;

    public override void onTick()
    {
        List<CharacterStats> targetsInside = colliderTriggers.getTargetsInside(targetsLayer);

        for (int i = 0; i < targetsInside.Count; i++)
        {
            applyEffectsTo(targetsInside[i]);
        }
    }

    private void applyEffectsTo(CharacterStats target)
    {
        for (int i = 0; i < effectsToApply.Length; i++)
        {
            target.addEffect(effectsToApply[i].getNewEffect());
        }
    }
}
