using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnableObject : MonoBehaviour {
    public HpBar bar;

    public int maxHp;
    public int hp;
    public int xp;
    public EnemysSpawner manager;
    public float id;
    public int zone;

    public void Init(EnemysSpawner manager, float id,int zone)
    {
        this.manager = manager;
        this.id = id;
        this.zone = zone;
    }
    public abstract void OnCollisionEnter2D(Collision2D col);

}
