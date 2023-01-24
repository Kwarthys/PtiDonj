using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    [SerializeField]
    private List<Effect> groundEffects = new List<Effect>();

    private List<IMyAnimator> animatedObjects = new List<IMyAnimator>();

    public static GameManager instance;

    public LayerMask groundLayer;

    public GameObject groundZonePositionHintPrefab;
    public GameObject groundDamagingZoneMarkerPrefab;
    public GameObject groundWarningZoneMarkerPrefab;
    public GameObject ExplosionVFXPrefab;

    private void Awake()
    {
        instance = this;
    }


    private void Update()
    {
        foreach (KeyValuePair<uint, CharacterStats> pair in PlayerManager.instance.playerCharacters)
        {
            pair.Value.updateDisplay();

            if(isServer)
            {
                pair.Value.updateStats();
            }
        }

        for (int i = 0; i < PlayerManager.instance.monstersList.Count; i++)
        {
            PlayerManager.instance.monstersList[i].monsterStats.updateDisplay();

            if(isServer)
            {
                PlayerManager.instance.monstersList[i].updateMonster();
            }
        }

        List<IMyAnimator> toRemove = null;

        for (int i = 0; i < animatedObjects.Count; i++)
        {
            bool keepAnimation = animatedObjects[i].updateAnimation();

            if (!keepAnimation)
            {
                if(toRemove == null)
                {
                    toRemove = new List<IMyAnimator>();
                }

                Debug.Log("Destroying at " + (i + 1) + "/" + animatedObjects.Count);
                toRemove.Add(animatedObjects[i]);
            }
        }

        if(toRemove != null)
        {
            for (int i = 0; i < toRemove.Count; i++)
            {
                animatedObjects[i].destroy();
                animatedObjects.RemoveAt(i);
                animatedObjects.Remove(toRemove[i]);
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
                for (int i = 0; i < removedEffects.Count; i++)
                {
                    if(removedEffects[i].associatedGameObject != null)
                    {
                        Debug.Log("GM Removing " + removedEffects[i].associatedGameObject.name);
                        NetworkServer.Destroy(removedEffects[i].associatedGameObject);
                    }
                }
            }
        }
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
        GameObject go = Instantiate(prefab, pos, Quaternion.identity);

        NetworkServer.Spawn(go);

        return go;
    }

    public void spawnFloatingTextFor(Vector3 pos, string text, NetworkIdentity attacker, NetworkIdentity damaged)
    {
        if(attacker != null)
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
                //Damaged character is a player, we need to show him the damaged taken
                //still unsure on how to display that, if if needs to be displayed at all
            }
        }
    }

    [TargetRpc]
    public void TargetRpcSpawnFloatingText(NetworkConnection target, Vector3 pos, string text)
    {
        FloatingTextManager.instance.spawnFloatingText(pos, text);
    }

    public void registerAnimatedObject(IMyAnimator animatedObject)
    {
        animatedObjects.Add(animatedObject);
    }
}
