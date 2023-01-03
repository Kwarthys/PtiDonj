using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyEffectOnColliderEffect : TickingEffect
{
    public Collider areaOfEffect;

    public EffectDescriptor[] effectsToApply;

    public LayerMask targetsLayer;

    private List<CharacterStats> targetInside;

    public ApplyEffectOnColliderEffect()
    {
        targetInside = new List<CharacterStats>();
    }

    public override void onTick()
    {
        for (int i = 0; i < targetInside.Count; i++)
        {
            applyEffectsTo(targetInside[i]);
        }
    }

    private void applyEffectsTo(CharacterStats target)
    {
        for (int i = 0; i < effectsToApply.Length; i++)
        {
            target.addEffect(effectsToApply[i].getNewEffect());
        }
    }

    private void onCharacterEnter(CharacterStats character)
    {
        if(character != null)
        {
            if(!targetInside.Contains(character))
            {
                targetInside.Add(character);
                Debug.Log(targetInside.Count + " chars inside.");
            }
        }
    }

    private void onCharacterExit(CharacterStats character)
    {
        if (character != null)
        {
            if (targetInside.Contains(character))
            {
                targetInside.Remove(character);
                Debug.Log(targetInside.Count + " chars inside.");
            }
        }
    }

    public void registerColliderTriggers(EffectColliderTrigger colliderTriggers)
    {
        colliderTriggers.onTriggerEnterCallback = onCharacterEnter;
        colliderTriggers.onTriggerExitCallback = onCharacterExit;
    }
}
