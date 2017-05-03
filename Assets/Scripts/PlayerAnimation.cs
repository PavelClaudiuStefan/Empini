using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerAnimation : NetworkBehaviour {

    private Animator anim;

    private bool playerMoving;
    private Vector2 lastMove;


    void Start () {
        anim = GetComponent<Animator>();
    }
	

	void Update () {

        if (!isLocalPlayer)
            return;

        playerMoving = false;
        float xAxis = Input.GetAxisRaw("Horizontal");
        float yAxis = Input.GetAxisRaw("Vertical");

        if (xAxis != 0 || yAxis != 0)
        {
            playerMoving = true;
            lastMove = new Vector2(xAxis, yAxis);
        }
        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        anim.SetBool("PlayerMoving", playerMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
    }
}
