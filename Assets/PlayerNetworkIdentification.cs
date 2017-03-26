using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerNetworkIdentification : NetworkBehaviour {

    public override void OnStartLocalPlayer()
    {
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<TestProjectile>().enabled = true;

        GetComponent<SpriteRenderer>().color = Color.yellow;

        Network.InitializeServer(32, 25000, false);
        Network.Connect("25.60.115.205", 25000);
    }
}
