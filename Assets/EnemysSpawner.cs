using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;

public class EnemysSpawner : NetworkBehaviour {

    [Serializable]
    public class SpawnZone
    {
        public float minX;
        public float maxX;
        public float minY;
        public float maxY;

        public int maxObjects;

        private List<SpawnableObject> objects = new List<SpawnableObject>();

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

    public Crate crateTarget;

    public SpawnZone[] cratesZones;
    public SpawnZone[] mobZones;

    private List<SpawnableObject> objectsClient = new List<SpawnableObject>();

    void Start()
    {
        if (isServer)
            StartCoroutine(WaitForRoundStart());
    }

    IEnumerator WaitForRoundStart()
    {
        while (!roundStarter.ready)
            yield return null;

        StartCoroutine(SpawnCrates());
       // StartCoroutine(SpawnMobs());

    }

    IEnumerator SpawnCrates()
    {
        while(true)
        {
            yield return new WaitForSeconds(5);
            for (int i = 0; i < cratesZones.Length; i++)
            {
                if(cratesZones[i].Check())
                    cratesZones[i].SpawnObject(transform, this, crateTarget,i);
            }
        }
    }
    IEnumerator SpawnMobs()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

        }
    }

    [ClientRpc]
    void RpcSpawnObject(Vector3 pos,int type,float id,int zone) // 0 - Crate 1 - Mob
    {
        if(!isServer)
        {
            if(type == 1)
            {
                GameObject temp = Instantiate(crateTarget.gameObject, transform);
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
                    cratesZones[obj.zone].Clear(obj.id);
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
