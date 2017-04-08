using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    private float closestResourceDistance;

    private GameObject[] trees;
    private GameObject closestResource;

    private PlayerStats playerStats;

    void Start()
    {
        playerStats = GetComponent<PlayerStats>();

    }
	void Update () {
 
        trees = GameObject.FindGameObjectsWithTag("Copac");
        closestResource = GetClosestResource(trees);
        closestResourceDistance = Vector3.Distance(closestResource.transform.position, transform.position);

        if (Input.GetKeyDown("space") && closestResourceDistance < 0.6)
        {
            Destroy(closestResource);
            playerStats.GiveWood(5);

            if (isServer)
                RpcGetWood(closestResource);
            else
                CmdGetWood(closestResource);
        }
        
	}
    GameObject GetClosestResource(GameObject[] resources)
    {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in resources)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    [Command]
    void CmdGetWood(GameObject tree)
    {
        playerStats.GiveWood(5);
        Destroy(closestResource);
    }

    [ClientRpc]
    void RpcGetWood(GameObject tree)
    {
        if (!isServer)
        {
            playerStats.GiveWood(5);
            Destroy(closestResource);
        }
    }

}
