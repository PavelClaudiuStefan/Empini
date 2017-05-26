using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class SpawnableObject : NetworkBehaviour {
    public HpBar bar;

    public int maxHp;
    public int hp;
    public int xp;
    public EnemysSpawner manager;
    public float id;
    public int zone;
    public bool server;
    public Vector3 posFromServer;
    public Quaternion rotFromServer;
    public Vector3 toPosFromServer;

    public void Init(EnemysSpawner manager, float id,int zone,bool server)
    {
        this.manager = manager;
        this.id = id;
        this.zone = zone;
        this.server = server;
    }
    public abstract void OnCollisionEnter2D(Collision2D col);
    public void SendMovement(Vector3 pos, Quaternion rot,Vector3 toPos)
    {
        posFromServer = pos;
        rotFromServer = rot;
        toPosFromServer = toPos;
    }
}
