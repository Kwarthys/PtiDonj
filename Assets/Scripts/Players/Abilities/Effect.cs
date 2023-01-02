using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Effect
{
    public string effectName = "Effect";

    public bool effectOnDuration = false;

    public GameObject associatedGameObject;

    //public Sprite effectSprite;

    [HideInInspector]
    public CharacterStats owner;
    [HideInInspector]
    public Vector3 effectWorldPos;

    public virtual void onStart() { }
    public virtual bool onTick() { return false; } //returns wether the effect must be kept alive, or removed
    public virtual void onEnd() { }

    /***
     * Returns the removed effects for complete deletion if needed
     */
    public static List<Effect> updateEffects(List<Effect> effects)
    {
        List<Effect> toRemove = null; //will stay unused the vast majority of calls, so keeping it null by default

        for (int i = 0; i < effects.Count; i++)
        {
            bool keepEffect = effects[i].onTick();

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
}
