using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    public AbilityTargeting abilityTargeting;

    public Sprite image;

    public float cooldown = 1;
    protected bool canCast = true;
    protected float cooldownDeltaTimeCounter = 0;

    public string abilityName = "Ability";
    public enum AbilityType { Basic, Combat, Movement, Finisher}

    public AbilityType type;

    public LayerMask targetLayer;

    public bool selfOnlyIfTarget = false;

    public EffectDescriptor[] targetEffects;
    public EffectDescriptor[] selfEffects;
    public EffectDescriptor[] groundEffects;

    [HideInInspector]
    public AbilityWidgetController associatedWidget;

    public short abilityIndex;
    protected AbilityManager manager;
    public CharacterStats ownerStats { get; protected set; }

    protected MovementEffector movementEffector;


    private void Start()
    {
        manager = GetComponentInParent<AbilityManager>();
        ownerStats = GetComponentInParent<CharacterStats>();

        movementEffector = GetComponent<MovementEffector>();
        if(movementEffector!=null) movementEffector.setupEffector(ownerStats);
    }

    public virtual void onAbilityUpdate(bool animationOnly)
    {
        if (canCast) return;

        if(cooldownDeltaTimeCounter == 0)
        {
            //first update after cast, allowing us to apply it on server for monsters, and on clients for players
            applyMovementEffector();
        }

        cooldownDeltaTimeCounter += Time.deltaTime;
        if(cooldownDeltaTimeCounter > cooldown)
        {
            canCast = true;
            cooldownDeltaTimeCounter = 0;
        }
    }

    public virtual void notifyAbilityFired()
    {
        canCast = false;
        cooldownDeltaTimeCounter = 0;
        Debug.Log("ApplyingMovemnt");
    }

    protected void applyMovementEffector()
    {
        //MovementEffected Added client-side
        if (movementEffector != null && !ownerStats.movementLocked())
        {
            AbilityTargetingData[] targets = abilityTargeting.findTargets(targetLayer);
            if (targets.Length > 0)
            {
                ownerStats.registerNewMovementEffector(movementEffector, targets[0]);
            }
        }
    }

    public virtual bool needsUpdate()
    {
        return !canCast;
    }

    public virtual bool canCastAbility()
    {
        return canCast;
    }

    public virtual bool tryCastAbility()
    {
        if (canCast)
        {
            computeTargetingAndApplyAbility();
            canCast = false;

            return true;
        }

        return false;
    }

    protected void computeTargetingAndApplyAbility()
    {
        AbilityTargetingData[] targets = abilityTargeting.findTargets(targetLayer);
        for (int i = 0; i < targets.Length; i++)
        {
            applyAbility(targets[i]);
        }
    }

    protected void applyAbility(AbilityTargetingData target)
    {
        if(selfEffects.Length > 0)
        {
            if (!selfOnlyIfTarget || target.charDidHit) //only apply if we don't need a target to do so, or if we do have a target
            {
                applyAbilitySelf(selfEffects);
            }
        }

        if(targetEffects.Length > 0)
        {
            if (target.charDidHit)
            {
                applyAbilityTarget(target.characterHit, targetEffects);
            }
        }

        if(groundEffects.Length > 0)
        {
            if(target.groundDidHit)
            {
                applyAbilityGround(target.groundHit, groundEffects);
            }
        }
    }

    private void applyAbilityTarget(CharacterStats target, EffectDescriptor[] targetEffects)
    {
        for (int i = 0; i < targetEffects.Length; ++i)
        {
            Effect effect = targetEffects[i].getNewEffect();
            effect.caster = ownerStats;
            target.addEffect(effect);
        }
    }

    private void applyAbilitySelf(EffectDescriptor[] selfEffects)
    {
        for (int i = 0; i < selfEffects.Length; ++i)
        {
            Effect effect = selfEffects[i].getNewEffect();
            effect.caster = ownerStats;
            manager.selfStats.addEffect(effect);
        }
    }

    private void applyAbilityGround(Vector3 posOnGround, EffectDescriptor[] groundEffects)
    {
        for (int i = 0; i < groundEffects.Length; ++i)
        {
            Effect effect = groundEffects[i].getNewEffect();
            effect.effectWorldPos = posOnGround;
            effect.caster = ownerStats;
            GameManager.instance.addGroundEffect(effect);
        }
    }

    public static void drawDebugCrossAtPoint(Vector3 worldPoint)
    {
        Debug.DrawLine(worldPoint + Vector3.left, worldPoint + Vector3.right * 2, Color.red, 5);
        Debug.DrawLine(worldPoint + Vector3.forward, worldPoint + Vector3.back * 2, Color.blue, 5);
        Debug.DrawLine(worldPoint + Vector3.up, worldPoint + Vector3.down * 2, Color.yellow, 5);
    }

    public virtual AbilityCooldownData getCooldownData()
    {
        AbilityCooldownData data = new AbilityCooldownData();

        if(canCast)
        {
            data.state = CooldownState.ready;
        }
        else
        {
            data.state = CooldownState.charging;
            data.fullCooldown = cooldown;
            data.cooldownSpent = cooldownDeltaTimeCounter;
        }

        return data;
    }
}
