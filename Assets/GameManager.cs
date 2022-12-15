using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Dictionary<uint, CharacterStats> charaters = new Dictionary<uint, CharacterStats>();

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
}
