using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AbilityCooldownData
{
    public CooldownState state;
    public float fullCooldown;
    public float cooldownSpent;
}

public enum CooldownState { ready, charging, casting}
