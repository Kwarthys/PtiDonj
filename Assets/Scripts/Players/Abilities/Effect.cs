using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    public string effectName = "Effect";

    public bool effectOnDuration = false;

    //public Sprite effectSprite;

    [HideInInspector]
    public CharacterStats owner;

    public virtual void onStart() { }
    public virtual bool onTick() { return false; } //returns wether the effect must be kept alive, or removed
    public virtual void onEnd() { }
}
