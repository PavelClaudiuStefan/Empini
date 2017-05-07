using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerNetworkIdentification : NetworkBehaviour {

    public PlayerController playerController;

    void Start()
    {
        EventManager.instance.Raise(new RegisterPlayer());
        playerController.Init();
    }

}
