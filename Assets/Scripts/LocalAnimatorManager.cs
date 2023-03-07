using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalAnimatorManager : MonoBehaviour
{
    private List<IMyAnimator> animatedLocalObjects = new List<IMyAnimator>();
    public static LocalAnimatorManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        if (ErrorMessageController.instance.needsAnimationUpdate())
        {
            ErrorMessageController.instance.updateAnimation();
        }

        foreach (KeyValuePair<uint, CharacterStats> pair in PlayerManager.instance.playerCharacters)
        {
            pair.Value.updateDisplay();
            pair.Value.updateAbilities(); //update cooldown systems for local display
        }

        foreach(KeyValuePair<uint, MonsterController> pair in PlayerManager.instance.monsterControllers)
        {
            pair.Value.monsterStats.updateDisplay();
            pair.Value.updateAnimator();
        }

        List<IMyAnimator> toRemove = null;

        for (int i = 0; i < animatedLocalObjects.Count; i++)
        {
            bool keepAnimation = animatedLocalObjects[i].updateAnimation();

            if (!keepAnimation)
            {
                if (toRemove == null)
                {
                    toRemove = new List<IMyAnimator>();
                }

                //Debug.Log("GM Destroying at " + (i + 1) + "/" + animatedObjects.Count);
                toRemove.Add(animatedLocalObjects[i]);
            }
        }


        if (toRemove != null)
        {
            for (int i = 0; i < toRemove.Count; i++)
            {
                //RpcSendDebugLog("GM Destoryed " + i);
                toRemove[i].destroyAnimator();
                animatedLocalObjects.Remove(toRemove[i]);
            }
        }
    }


    public void registerAnimatedLocalObject(IMyAnimator animatedObject)
    {
        animatedLocalObjects.Add(animatedObject);
    }
}
