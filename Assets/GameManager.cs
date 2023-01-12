using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    [SerializeField]
    private Dictionary<uint, CharacterStats> characters = new Dictionary<uint, CharacterStats>();

    [SerializeField]
    public List<MonsterController> monstersList = new List<MonsterController>(); //for now public and filled from editor, but will be filled automatically in the future

    [SerializeField]
    private List<Effect> groundEffects = new List<Effect>();

    private List<IMyAnimator> animatedObjects = new List<IMyAnimator>();

    public static GameManager instance;

    [HideInInspector]
    public Transform localPlayerTransform;

    public LayerMask groundLayer;

    public GameObject floatingTextPrefab;
    public Transform floatingTextHolder;

    private void Awake()
    {
        instance = this;
    }


    private void Update()
    {
        foreach (KeyValuePair<uint, CharacterStats> pair in characters)
        {
            pair.Value.updateStats();
        }

        for (int i = 0; i < monstersList.Count; i++)
        {
            monstersList[i].updateMonster();
        }

        for (int i = 0; i < animatedObjects.Count; i++)
        {
            if(!animatedObjects[i].updateAnimation())
            {
                animatedObjects[i].destroy();
                animatedObjects.RemoveAt(i);
                i--;
            }
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
                    NetworkServer.Destroy(removedEffects[i].associatedGameObject);
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
        characters.Add(netId, character);
    }

    public void removeCharacter(uint netId)
    {
        characters.Remove(netId);
    }

    public CharacterStats getCharacter(uint netId)
    {
        return characters[netId];
    }

    public CharacterStats getRandomCharacter()
    {
        if (characters.Count == 0) return null;

        List<CharacterStats> charList = new List<CharacterStats>();

        foreach(KeyValuePair<uint, CharacterStats> entry in characters)
        {
            charList.Add(entry.Value);
        }

        int r = (int)(Random.value * charList.Count);

        return charList[r];
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

    public void spawnFloatingText(Vector3 pos, string text)
    {
        GameObject go = Instantiate(floatingTextPrefab, pos + (localPlayerTransform.position - pos).normalized * 2f, Quaternion.identity, floatingTextHolder);

        FloatingTextController controller = go.GetComponent<FloatingTextController>();
        controller?.setText(text);

        FloatingTextAnimator animator = go.GetComponent<FloatingTextAnimator>();
        animator.setupAnimator(localPlayerTransform);
        animatedObjects.Add(animator);

        NetworkServer.Spawn(go);
    }
}
