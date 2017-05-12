using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;

public class EnemysSpawner : NetworkBehaviour {

    [Serializable]
    public class SpawnZone
    {
        public Zone zone;
        public SpawnableObject target;

        float minX;
        float maxX;
        float minY;
        float maxY;

        public int maxObjects;

        private List<SpawnableObject> objects = new List<SpawnableObject>();

        public void Init()
        {
            minX = zone.minX;
            minY = zone.minY;
            maxX = zone.maxX;
            maxY = zone.maxY;
        }

        public bool Check()
        {
            if (objects.Count < maxObjects)
                return true;

            return false;
        }

        public void SpawnObject(Transform father,EnemysSpawner manager, SpawnableObject e,int zone)
        {
       
            float id = UnityEngine.Random.Range(0.0f, 100000.0f);
            e.Init(manager, id, zone);
            GameObject temp = Instantiate(e.gameObject, father);
            Vector3 pos = RandomPoint();
            temp.transform.position = pos;
            objects.Add(temp.GetComponent<SpawnableObject>());

            if (e is Crate)
                manager.RpcSpawnObject(pos, 1, id, zone);
            if (e is Enemy)
                manager.RpcSpawnObject(pos, 2, id, zone);
        }

        Vector3 RandomPoint()
        {
            float x = UnityEngine.Random.Range(minX, maxX);
            float y = UnityEngine.Random.Range(minY, maxY);
            float z = 0;

            return new Vector3(x, y, z);
        }

        public void Clear(float id)
        {
            SpawnableObject temp = null;
            foreach (var obj in objects)
            {
                if(obj.id == id)
                {
                    temp = obj;
                    break;
                }
            }
            if(temp != null)
            {
                objects.Remove(temp);
                Destroy(temp.gameObject);
            }
        }
    }

    public RoundStarter roundStarter;

    //public Crate crateTarget;
    //public Enemy enemyTarget;

    public SpawnZone[] cratesZones;
    public SpawnZone[] mobZones;

    private List<SpawnableObject> objectsClient = new List<SpawnableObject>();

    private List<SpawnableObject> objectsClientCrates = new List<SpawnableObject>();
    private List<SpawnableObject> objectsClientMobs = new List<SpawnableObject>();


    void Start()
    {
        foreach (var z in cratesZones)
            objectsClientCrates.Add(z.target);
        foreach (var z in mobZones)
            objectsClientMobs.Add(z.target);

        if (isServer)
            StartCoroutine(WaitForRoundStart());
    }

    IEnumerator WaitForRoundStart()
    {
        while (!roundStarter.ready)
            yield return null;

        foreach (var z in cratesZones)
            z.Init();
        foreach (var z in mobZones)
            z.Init();

        StartCoroutine(SpawnCrates());
        StartCoroutine(SpawnMobs());

    }

    IEnumerator SpawnCrates()
    {
        while(true)
        {
            yield return new WaitForSeconds(5);
            for (int i = 0; i < cratesZones.Length; i++)
            {
                if (cratesZones[i].Check())
                    cratesZones[i].SpawnObject(transform, this, cratesZones[i].target, i);//crateTarget, i);
            }
        }
    }
    IEnumerator SpawnMobs()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            for (int i = 0; i < mobZones.Length; i++)
            {
                if (mobZones[i].Check())
                    mobZones[i].SpawnObject(transform, this, mobZones[i].target, i);//enemyTarget, i);
            }
        }
    }

    [ClientRpc]
    void RpcSpawnObject(Vector3 pos,int type,float id,int zone) // 1 - Crate 2 - Mob
    {
        if(!isServer)
        {
            if (type == 1)
            {
                GameObject temp = Instantiate(objectsClientCrates[zone].gameObject, transform);
                temp.transform.position = pos;
                temp.GetComponent<SpawnableObject>().Init(this, id, zone);
                objectsClient.Add(temp.GetComponent<SpawnableObject>());
            }
            if(type == 2)
            {
                GameObject temp = Instantiate(objectsClientMobs[zone].gameObject, transform);
                temp.transform.position = pos;
                temp.GetComponent<SpawnableObject>().Init(this, id, zone);
                objectsClient.Add(temp.GetComponent<SpawnableObject>());
            }

        }
    }

    public void ObjectHitted(Collision2D col,SpawnableObject obj)
    {
        if (isServer)
        {
            if (col.gameObject.tag == "Bullet")
            {
                obj.hp -= col.gameObject.GetComponent<ProjectileStats>().damage;
                obj.bar.SetBar(obj.hp, obj.maxHp);

                if (obj.hp <= 0)
                {
                    col.gameObject.GetComponent<ProjectileStats>().playerStats.PlayerXp += obj.xp;

                    if(obj is Crate)
                        cratesZones[obj.zone].Clear(obj.id);
                    if (obj is Enemy)
                        mobZones[obj.zone].Clear(obj.id);
                }

                Destroy(col.gameObject);

                RpcObjectHitted(obj.hp,obj.maxHp,obj.id);
            }
        }

        if (col.gameObject.tag == "Bullet")
        {
            try
            {
                Destroy(col.gameObject);
            }
            catch { };
        }
    }

    [ClientRpc]
    void RpcObjectHitted(int hp,int maxHp,float id)
    {
        if(!isServer)
        {
            SpawnableObject temp = null;
            foreach (var obj in objectsClient)
                if(obj.id == id)
                {
                    obj.hp = hp;
                    obj.bar.SetBar(hp, maxHp);

                    if(hp <= 0)
                    {
                        temp = obj;
                    }
                    break;
                }

            if(temp != null)
            {
                objectsClient.Remove(temp);
                Destroy(temp.gameObject);
            }
        }

    }

}
