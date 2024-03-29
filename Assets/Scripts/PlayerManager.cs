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
    public Dictionary<uint, MonsterController> monsterControllers = new Dictionary<uint,MonsterController>(); //for now public and filled from editor, but will be filled automatically in the future

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

    public void registerMonster(uint netId, MonsterController monsterController)
    {
        monsterControllers.Add(netId, monsterController);
    }

    public void removeMonster(uint netId)
    {
        monsterControllers.Remove(netId);
    }

    public CharacterStats getCharacter(uint netId)
    {
        if(playerCharacters.ContainsKey(netId))
        {
            return playerCharacters[netId];
        }

        if(monsterControllers.ContainsKey(netId))
        {
            return monsterControllers[netId].monsterStats;
        }

        return null;
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

    public CharacterStats[] getNRandomCharacters(int maxNumber)
    {
        if (playerCharacters.Count == 0) return null;

        List<CharacterStats> charList = new List<CharacterStats>();

        foreach (KeyValuePair<uint, CharacterStats> entry in playerCharacters)
        {
            charList.Add(entry.Value);
        }

        while(charList.Count > maxNumber)
        {
            int randomIndex = (int)(Random.value * charList.Count);
            charList.RemoveAt(randomIndex);
        }

        return charList.ToArray();
    }

    public bool isPlayerRegistered(uint netID)
    {
        return playerCharacters.ContainsKey(netID);
    }
}
