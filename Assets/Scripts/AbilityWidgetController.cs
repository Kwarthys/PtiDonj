using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityWidgetController : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private TMP_Text textMesh;

    private Ability associatedAbility;

    private float deltaTimeCounter;

    public bool needsUpdate { get; private set; } = false;

    public void setup(Ability ability)
    {
        associatedAbility = ability;
        ability.associatedWidget = this;

        image.material = new Material(image.material);

        if (ability.image != null)
        {
            image.material.mainTexture = ability.image.texture;

            needsUpdate = false;
            resetWidget();
        }
    }

    public void updateAnimation()
    {
        if (!needsUpdate) return;

        deltaTimeCounter += Time.deltaTime;

        AbilityCooldownData data = associatedAbility.getCooldownData();

        if(data.state == CooldownState.ready)
        {
            needsUpdate = false;
            resetWidget();
        }
        else if(data.state == CooldownState.casting)
        {
            image.material.SetFloat("_HideAmount", 1);
            textMesh.SetText("");
        }
        else if(data.state == CooldownState.charging)
        {
            float timeLeft = data.fullCooldown - data.cooldownSpent;
            float roundedTimeLeft = Mathf.Ceil(timeLeft);
            float relativeRecharge = 1 - (data.cooldownSpent / data.fullCooldown);

            textMesh.SetText(roundedTimeLeft.ToString());
            image.material.SetFloat("_HideAmount", relativeRecharge);
        }
    }

    private void resetWidget()
    {
        image.material.SetFloat("_HideAmount", 0);
        textMesh.SetText("");
    }

    public void abilityFired()
    {
        needsUpdate = true;
        deltaTimeCounter = 0;
    }

}
