using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedApplyEffectOnColliderEffect : OnTimeEffect
{
    public LayerMask targetsLayer;

    public EffectDescriptor[] effectsToApply;

    public ColliderTriggerHandler colliderTriggers;

    public override void onEnd()
    {
        List<CharacterStats> characters = colliderTriggers.getTargetsInside(targetsLayer);

        for (int i = 0; i < characters.Count; i++)
        {
            applyEffectsTo(characters[i]);
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
