using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerSetup : NetworkBehaviour
{
    public Behaviour[] toDisable;

    public GameObject[] toDestroyOnLocal;

    public Transform playerCameraHolder;

    public CharacterStats playerStats;
    public NetworkIdentity networkIdentity;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            for (int i = 0; i < toDisable.Length; ++i)
            {
                toDisable[i].enabled = false;
            }
        }
        else
        {
            for (int i = 0; i < toDestroyOnLocal.Length; i++)
            {
                Destroy(toDestroyOnLocal[i]);
            }

            Transform theCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
            theCamera.SetParent(playerCameraHolder);
            theCamera.localRotation = Quaternion.identity;
            theCamera.localPosition = Vector3.zero;

            Cursor.lockState = CursorLockMode.Locked;

            GameManager.instance.localPlayerTransform = transform;

            playerStats.registerNewDisplayManager(GameManager.instance.localPlayerHealthBar);
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        GameManager.instance.registerCharacter(networkIdentity.netId, playerStats);
    }

    public override void OnStopClient()
    {
        base.OnStopClient();

        GameManager.instance.removeCharacter(networkIdentity.netId);
    }
}
