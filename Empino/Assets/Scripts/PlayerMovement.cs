using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float _moveSpeed;

    private Rigidbody2D myRigidbody;

	// Use this for initialization
	void Start () {
        myRigidbody =  GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        myRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * _moveSpeed;
	}
}
