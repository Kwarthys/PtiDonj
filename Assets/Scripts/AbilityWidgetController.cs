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
            image.material.SetFloat("_HideAmount", 0);
            textMesh.SetText("");
        }
    }

    public void updateAnimation()
    {
        if (!needsUpdate) return;

        deltaTimeCounter += Time.deltaTime;

        float cooldown = associatedAbility.getCooldown();

        image.material.SetFloat("_HideAmount", 1-(deltaTimeCounter / cooldown));

        float roundedTimeLeft = Mathf.Ceil(cooldown - deltaTimeCounter);

        textMesh.SetText(roundedTimeLeft.ToString());

        if(cooldown < deltaTimeCounter)
        {
            needsUpdate = false;
            image.material.SetFloat("_HideAmount", 0);
            textMesh.SetText("");
        }
    }

    public void abilityFired()
    {
        needsUpdate = true;
        deltaTimeCounter = 0;
    }

}
