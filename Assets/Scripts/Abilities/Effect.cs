using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Effect
{
    public string effectName = "Effect";

    public bool effectOnDuration = false;

    public GameObject associatedGameObject;

    public CharacterStats owner;
    public CharacterStats caster; //for future logging

    public Vector3 effectWorldPos;

    public virtual void onStart() { }
    public virtual void onTick() { }
    public virtual void onEnd() { }

    public virtual bool onUpdate() { return false; }

    public Effect(string effectName, bool effectOnDuration)
    {
        this.effectName = effectName;
        this.effectOnDuration = effectOnDuration;
    }


    /// <summary>
    /// Updates the effects in the list and returns the removed effects for complete and proper removal if needed
    /// </summary>
    public static List<Effect> updateEffects(List<Effect> effects)
    {
        List<Effect> toRemove = null; //will stay unused the vast majority of calls, so keeping it null by default

        for (int i = 0; i < effects.Count; i++)
        {
            bool keepEffect = effects[i].updateEffect();

            if (!keepEffect)
            {
                effects[i].onEnd();

                if (toRemove == null)
                {
                    toRemove = new List<Effect>();
                }
                toRemove.Add(effects[i]);
            }
        }

        if (toRemove != null)
        {
            for (int i = 0; i < toRemove.Count; ++i)
            {
                effects.Remove(toRemove[i]);
            }
        }

        return toRemove;
    }

    /// <summary>
    /// Manages timed behaviour
    /// </summary>
    /// <returns>True, False if effect has completed and must be removed</returns>
    protected virtual bool updateEffect()
    {
        return false;
    }

    protected void inflictDamage(float amount)
    {
        owner.takeDamage(amount, caster.networkIdentity, owner.networkIdentity);
    }

    protected void sendHealing(float amount)
    {
        owner.receiveHealing(amount, caster.networkIdentity, owner.networkIdentity);
    }

    protected void applyEffectTo(CharacterStats target, EffectDescriptor toApply)
    {
        Effect e = toApply.getNewEffect();
        e.caster = caster;
        target.addEffect(e);
    }

    protected void applyEffectsTo(CharacterStats target, EffectDescriptor[] toApply)
    {
        for (int i = 0; i < toApply.Length; i++)
        {
            applyEffectTo(target, toApply[i]);
        }        
    }
}