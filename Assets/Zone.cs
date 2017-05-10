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

        minX = col.points[0].x + 0.5f;
        minY = col.points[0].y + 0.5f;

        maxY = col.points[2].y - 0.5f;
        maxX = col.points[2].x - 0.5f;

    }
	
	void Update () {
		
	}
}
