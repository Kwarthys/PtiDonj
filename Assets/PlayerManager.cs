using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    private void Awake()
    {
        instance = this;
    }

    [SerializeField]
    public Dictionary<uint, CharacterStats> playerCharacters = new Dictionary<uint, CharacterStats>();

    [SerializeField]
    public List<MonsterController> monstersList = new List<MonsterController>(); //for now public and filled from editor, but will be filled automatically in the future

    [HideInInspector]
    public Transform localPlayerTransform;
    public SelfStatsDisplayManager localPlayerHealthBar;

    public void registerCharacter(uint netId, CharacterStats character)
    {
        playerCharacters.Add(netId, character);
    }

    public void removeCharacter(uint netId)
    {
        playerCharacters.Remove(netId);
    }

    public CharacterStats getCharacter(uint netId)
    {
        return playerCharacters[netId];
    }

    public CharacterStats getRandomCharacter()
    {
        if (playerCharacters.Count == 0) return null;

        List<CharacterStats> charList = new List<CharacterStats>();

        foreach (KeyValuePair<uint, CharacterStats> entry in playerCharacters)
        {
            charList.Add(entry.Value);
        }

        int r = (int)(Random.value * charList.Count);

        return charList[r];
    }

    public bool isPlayerRegistered(uint netID)
    {
        return playerCharacters.ContainsKey(netID);
    }
}
