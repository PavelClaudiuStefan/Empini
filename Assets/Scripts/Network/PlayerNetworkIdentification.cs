using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TeleportOnPosition : GameEvent
{
    public Vector3 onPos;
    public float id;
    public TeleportOnPosition(Vector3 pos,float id)
    {
        onPos = pos;
        this.id = id;
    }
}

public class PlayerNetworkIdentification : NetworkBehaviour {

    public PlayerController playerController;
    public float id;
    public Vector3 startPos;

    void Start()
    {
        if (isServer)
        {
            EventManager.instance.AddListener<TeleportOnPosition>(TeleportOnPos);
            id = Random.Range(0f, 10000f);
            RpcSendId(id);
            EventManager.instance.Raise(new RegisterPlayer(id));
        }

        playerController.Init();
    }
    void TeleportOnPos(TeleportOnPosition e)
    {
        if (e.id == id)
        {
            startPos = e.onPos;
            transform.position = e.onPos;

            RpcTeleport(e.onPos);
        }
    }

    [ClientRpc]
    void RpcTeleport(Vector3 pos)
    {
        startPos = pos;
        transform.position = pos;
    }

    [ClientRpc]
    void RpcSendId(float id)
    {
        if(!isServer)
        {
            this.id = id;
        }
    }

}
