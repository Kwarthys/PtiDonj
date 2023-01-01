using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyEffectOnColliderEffect : Effect
{
    public Collider areaOfEffect;

    public EffectDescriptor[] effectsToApply;

    public LayerMask targetsLayer;

    public float effectDuration;
    private float effectStart = -1;

    public float effectTickCooldown;
    private float effectLastTick = -1;

    private List<CharacterStats> targetInside;

    public override void onStart()
    {
        effectStart = Time.realtimeSinceStartup;

        targetInside = new List<CharacterStats>();
    }

    public override bool onTick()
    {
        if (targetInside.Count > 0)
        {
            if (Time.realtimeSinceStartup - effectLastTick > effectTickCooldown)
            {
                effectLastTick = Time.realtimeSinceStartup;

                for (int i = 0; i < targetInside.Count; i++)
                {
                    applyEffectsTo(targetInside[i]);
                }
            }
        }        

        return Time.realtimeSinceStartup - effectStart < effectDuration + effectTickCooldown / 2; //making sure last tick will always proc   
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
