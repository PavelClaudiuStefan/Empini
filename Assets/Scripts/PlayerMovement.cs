using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float _moveSpeed;

    private Rigidbody2D myRigidbody;

    private PlayerStats playerStats;

	void Start () {
        
        Camera.main.GetComponent<CameraController>().InjectPlayer(gameObject);
        myRigidbody =  GetComponent<Rigidbody2D>();
	}

    public void Init(PlayerStats playerStats)
    {
        this.playerStats = playerStats;

    }

	void Update () {

        if (playerStats != null)
            myRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * playerStats.MovementSpeed;
        else
        {
            myRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * 4;
            Debug.Log("PlayerStats is null from PlayerController !!! "); // In caz de ceva probleme 
        }

    }
}
