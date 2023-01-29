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

        Vector2 data = associatedAbility.getCooldownData();
        float cooldown = data.x;
        float relativeCooldown = data.y;

        image.material.SetFloat("_HideAmount", relativeCooldown);
        textMesh.SetText(cooldown.ToString());

        if(cooldown < 0)
        {
            needsUpdate = false;
            image.material.SetFloat("_HideAmount", 0);
            textMesh.SetText("");
        }
    }

    public void abilityFired()
    {
        needsUpdate = true;
    }

}
