using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerNetworkIdentification : NetworkBehaviour {

    public PlayerController playerController;

    void Start()
    {
        playerController.Init();
    }
   /* public override void OnStartLocalPlayer()
    {
        playerController.Init();

        //GetComponent<SpriteRenderer>().color = Color.yellow;

        //Network.InitializeServer(32, 25000, false);
        //Network.Connect("25.60.115.205", 25000);

    }*/
}
