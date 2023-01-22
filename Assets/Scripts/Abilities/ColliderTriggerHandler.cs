using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTriggerHandler : MonoBehaviour
{
    public delegate void ColliderCallback(CharacterStats collided);

    public ColliderCallback onTriggerEnterCallback = null;
    public ColliderCallback onTriggerExitCallback = null;

    public ZonePosHintController associatedHintZoneController;

    private List<CharacterStats> targetsInside = new List<CharacterStats>();

    public List<CharacterStats> getTargetsInside(LayerMask mask)
    {
        List<CharacterStats> list = new List<CharacterStats>();

        for (int i = 0; i < targetsInside.Count; i++)
        {
            if (mask == (mask | (1 << targetsInside[i].gameObject.layer))) //this should check if target is actually in the effect's layer mask
            {
                list.Add(targetsInside[i]);
            }
        }

        return list;
    }

    private void OnTriggerEnter(Collider other)
    {
        CharacterStats character = other.GetComponentInParent<CharacterStats>();
        
        if (character != null)
        {
            if (!targetsInside.Contains(character))
            {
                targetsInside.Add(character);

                if(character.transform == GameManager.instance.localPlayerTransform)
                {
                    associatedHintZoneController.setPlayerInside(true);
                }
            }
        }

        onTriggerEnterCallback?.Invoke(character);
    }

    private void OnTriggerExit(Collider other)
    {
        CharacterStats character = other.GetComponentInParent<CharacterStats>(); 
        
        if (character != null)
        {
            if (targetsInside.Contains(character))
            {
                targetsInside.Remove(character);

                if (character.transform == GameManager.instance.localPlayerTransform)
                {
                    associatedHintZoneController.setPlayerInside(false);
                }
            }
        }

        onTriggerExitCallback?.Invoke(character);
    }
}
