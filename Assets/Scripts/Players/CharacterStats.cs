using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class CharacterStats : NetworkBehaviour
{
    [SerializeField]
    [SyncVar]
    private float life = 100;

    [SerializeField]
    private List<Effect> effects = new List<Effect>();

    public NetworkIdentity networkIdentity;

    public TextMeshPro lifeText;

    public void takeDamage(float amount)
    {
        life -= amount;

        //Debug.Log("Took " + amount + " damage.");

        lifeText.text = life.ToString();
    }

    public void removeEffect(Effect effect)
    {
        effects.Remove(effect);
    }

    public void addEffect(Effect effect)
    {
        effect.owner = this;
        effect.onStart();

        if(effect.effectOnDuration)
        {
            effects.Add(effect);
        }
    }

    private void Start()
    {
        lifeText.text = life.ToString();
    }

    /***
     * Temporary test
     */
    public void Update()
    {
        updateEffects();
    }

    public void updateEffects()
    {
        List<Effect> toRemove = null; //will stay unused the vast majority of calls, so keeping it null by default

        for (int i = 0; i < effects.Count; i++)
        {
            bool keepEffect = effects[i].onTick();

            if(!keepEffect)
            {
                effects[i].onEnd();

                if (toRemove == null)
                {
                    toRemove = new List<Effect>();
                }

                toRemove.Add(effects[i]);
            }
        }

        if(toRemove != null)
        {
            for (int i = 0; i < toRemove.Count; ++i)
            {
                effects.Remove(toRemove[i]);
            }
        }
    }


    public override void OnStartClient()
    {
        base.OnStartClient();

        GameManager.instance.registerCharacter(networkIdentity.netId, this);
    }

    public override void OnStopClient()
    {
        base.OnStopClient();

        GameManager.instance.removeCharacter(networkIdentity.netId);
    }
}
