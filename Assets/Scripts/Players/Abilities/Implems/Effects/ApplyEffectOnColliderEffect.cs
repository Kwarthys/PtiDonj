using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyEffectOnColliderEffect : Effect
{
    public Collider areaOfEffect;

    public Effect[] effectsToApply;

    public LayerMask targetsLayer;

    public float effectDuration;
    private float effectStart = -1;

    public float effectTickCooldown;
    private float effectLastTick = -1;

    private bool registeringEnters = false;

    private List<CharacterStats> targetInside;

    public override void onStart()
    {
        effectStart = Time.realtimeSinceStartup;

        registeringEnters = true;

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
            target.addEffect(effectsToApply[i]);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!registeringEnters) return;

        CharacterStats character = other.GetComponent<CharacterStats>();

        if(character != null)
        {
            if(!targetInside.Contains(character))
            {
                targetInside.Add(character);
            }
        }

        Debug.Log($"{other.transform.name} just entered.");
    }

    private void OnTriggerExit(Collider other)
    {
        CharacterStats character = other.GetComponent<CharacterStats>();

        if (character != null)
        {
            if (targetInside.Contains(character))
            {
                targetInside.Remove(character);
            }
        }

        Debug.Log($"{other.transform.name} just exited.");
    }
}
