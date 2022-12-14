using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    public string effectName = "Effect";
    public float effectDuration;
    public float effectTickCooldown;
    public float effectLastTick;

    public Sprite effectSprite;

    [HideInInspector]
    public CharacterStats owner;

    public virtual void onStart() { }
    public virtual void onTick() { }
    public virtual void onEnd() { }
}
