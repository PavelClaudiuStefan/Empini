using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;



public class PlayerMovement : NetworkBehaviour {

    public float _moveSpeed;

    public GameObject _bulletPrefab;
    public Transform _bulletSpawn;

    private Rigidbody2D myRigidbody;


	void Start () {
        Camera.main.GetComponent<CameraController>().InjectPlayer(gameObject);
        myRigidbody =  GetComponent<Rigidbody2D>();
	}
	

	void Update () {

        if (!isLocalPlayer) {
            return;
        }

        myRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * _moveSpeed;

        if (Input.GetKeyDown(KeyCode.Space)) {
            CmdFire();
        }
    }

    public override void OnStartLocalPlayer()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
    }

    [Command]
    void CmdFire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            _bulletPrefab,
            _bulletSpawn.position,
            _bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

        // Spawn the bullet on the Clients
        NetworkServer.Spawn(bullet);

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);
    }
}
