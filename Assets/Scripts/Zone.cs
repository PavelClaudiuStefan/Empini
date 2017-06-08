using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour {

    private EdgeCollider2D col;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

	void Start () {
        col = GetComponent<EdgeCollider2D>();
        
        minX = transform.position.x + col.points[0].x + 0.5f;
        minY = transform.position.y + col.points[0].y + 0.5f;

        maxY = transform.position.y + col.points[2].y - 0.5f;
        maxX = transform.position.x + col.points[2].x - 0.5f;
        Debug.Log(minX + " " + minY + " " + maxY + " " + maxX);

    }
	
	void Update () {
		
	}
}
