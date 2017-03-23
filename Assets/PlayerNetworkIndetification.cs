using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerNetworkIndetification : NetworkBehaviour {

    public override void OnStartLocalPlayer()
    {
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<TestProjectile>().enabled = true;
    }
}
