using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Dictionary<uint, CharacterStats> charaters = new Dictionary<uint, CharacterStats>();

    [SerializeField]
    private List<Effect> groundEffects = new List<Effect>();

    public static GameManager instance;

    public Transform localPlayerTransform;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        foreach(KeyValuePair<uint, CharacterStats> pair in charaters)
        {
            pair.Value.updateStats();
        }
    }

    private void FixedUpdate()
    {
        List<Effect> removedEffects = Effect.updateEffects(groundEffects);
        if(removedEffects != null)
        {
            for (int i = 0; i < removedEffects.Count; i++)
            {
                if(removedEffects[i].associatedGameObject != null)
                {
                    Destroy(removedEffects[i].associatedGameObject);
                }
                else
                {
                    Debug.LogWarning("GroundEffect " + removedEffects[i].effectName + " has no associated GameObject");
                }
            }
        }
    }

    public void registerCharacter(uint netId, CharacterStats character)
    {
        charaters.Add(netId, character);
    }

    public void removeCharacter(uint netId)
    {
        charaters.Remove(netId);
    }

    public CharacterStats getCharacter(uint netId)
    {
        return charaters[netId];
    }
    public void removeGroundEffect(Effect effect)
    {
        groundEffects.Remove(effect);
    }

    public void addGroundEffect(Effect effect)
    {
        effect.onStart();

        if (effect.effectOnDuration)
        {
            groundEffects.Add(effect);
        }
    }

    public GameObject spawnPrefab(GameObject prefab, Vector3 pos)
    {
        return Instantiate(prefab, pos, Quaternion.identity);
    }
}
