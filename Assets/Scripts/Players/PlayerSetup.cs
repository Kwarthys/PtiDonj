using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerSetup : NetworkBehaviour
{
    public Behaviour[] toDisable;

    public Transform playerCameraHolder;

    private void Start()
    {
        if(!isLocalPlayer)
        {
            for(int i = 0; i < toDisable.Length; ++i)
            {
                toDisable[i].enabled = false;
            }
        }
        else
        {
            Transform theCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
            theCamera.SetParent(playerCameraHolder);
            theCamera.localRotation = Quaternion.identity;
            theCamera.localPosition = Vector3.zero;
        }
    }
}
