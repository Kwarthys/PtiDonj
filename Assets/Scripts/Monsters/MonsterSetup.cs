using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MonsterSetup : NetworkBehaviour
{
    public override void OnStartClient()
    {
        base.OnStartClient();

        PlayerManager.instance.registerMonster(GetComponent<NetworkIdentity>().netId, GetComponent<MonsterController>());

        GetComponent<CharacterStats>().isPlayer = false;
    }

    public override void OnStopClient()
    {
        base.OnStopClient();

        PlayerManager.instance.removeMonster(GetComponent<NetworkIdentity>().netId);
    }
}
