using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    [SerializeField]
    private List<Effect> groundEffects = new List<Effect>();

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(isServer)
        {
            foreach (KeyValuePair<uint, CharacterStats> pair in PlayerManager.instance.playerCharacters)
            {
                pair.Value.updateStats();
            }

            for (int i = 0; i < PlayerManager.instance.monstersList.Count; i++)
            {
                PlayerManager.instance.monstersList[i].updateMonster();
            }
        }
    }

    private void FixedUpdate()
    {
        if(isServer)
        {
            List<Effect> removedEffects = Effect.updateEffects(groundEffects);
            if(removedEffects != null)
            {
                //RpcSendDebugLog(removedEffects.Count + " GM effects to remove");

                for (int i = 0; i < removedEffects.Count; i++)
                {
                    if(removedEffects[i].associatedGameObject != null)
                    {
                        //RpcSendDebugLog("Network destroyed " + removedEffects[i].associatedGameObject.name);
                        //Debug.Log("GM Removing " + removedEffects[i].associatedGameObject.name);
                        NetworkServer.Destroy(removedEffects[i].associatedGameObject);
                    }
                    else
                    {
                        //RpcSendDebugLog("Prevented a network destroy");
                    }
                }
            }
        }
    }

    [ClientRpc]
    public void RpcSendDebugLog(string debugString) //debug
    {
        Debug.Log(debugString);
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
        GameObject go = Instantiate(prefab, pos, Quaternion.identity);

        NetworkServer.Spawn(go);

        return go;
    }

    public void spawnFloatingTextFor(Vector3 pos, string text, NetworkIdentity attacker, NetworkIdentity damaged)
    {
        if(attacker != null && attacker != damaged) //self injuries displayed as damage taken, not dealt
        {
            if(PlayerManager.instance.isPlayerRegistered(attacker.netId))
            {
                //Attacker is a player, we need to show him his damages
                TargetRpcSpawnFloatingText(attacker.connectionToClient, pos, text);
            }
        }

        if(damaged != null)
        {
            if(PlayerManager.instance.isPlayerRegistered(damaged.netId))
            {
                //Damaged character is a player, we need to show him the damage taken
                //still unsure on how to display that, if if needs to be displayed at all
            }
        }
    }

    [TargetRpc]
    public void TargetRpcSpawnFloatingText(NetworkConnection target, Vector3 pos, string text)
    {
        FloatingTextManager.instance.spawnFloatingText(pos, text);
    }
}
