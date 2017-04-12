using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private float closestResourceDistance;

    private GameObject[] trees;
    private GameObject closestResource;
    private List<GameObject> models;

    void Start () {

    }
	
	
	void Update () {
 
        trees = GameObject.FindGameObjectsWithTag("Copac");
        closestResource = GetClosestResource(trees);
        closestResourceDistance = Vector3.Distance(closestResource.transform.position, transform.position);

        if (Input.GetKeyDown("space") && closestResourceDistance < 0.6)
        {
            //Debug.Log(closestResource);
            Debug.Log(closestResourceDistance);
            Destroy(closestResource);
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


}
