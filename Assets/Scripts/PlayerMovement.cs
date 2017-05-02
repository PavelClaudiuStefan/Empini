using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float _moveSpeed;

    private Rigidbody2D myRigidbody;

	void Start () {
        
        Camera.main.GetComponent<CameraController>().InjectPlayer(gameObject);
        myRigidbody =  GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        myRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * _moveSpeed;
	}
}
