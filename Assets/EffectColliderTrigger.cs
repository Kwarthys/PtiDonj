using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectColliderTrigger : MonoBehaviour
{
    public delegate void ColliderCallback(CharacterStats collided);

    public ColliderCallback onTriggerEnterCallback;
    public ColliderCallback onTriggerExitCallback;

    private void OnTriggerEnter(Collider other)
    {
        CharacterStats character = other.GetComponentInParent<CharacterStats>();

        onTriggerEnterCallback?.Invoke(character);
    }

    private void OnTriggerExit(Collider other)
    {
        CharacterStats character = other.GetComponentInParent<CharacterStats>();

        onTriggerExitCallback?.Invoke(character);
    }
}
