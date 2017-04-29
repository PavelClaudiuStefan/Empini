using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    public PlayerStats playerStats;
    public PlayerMovement playerMovement;
    public PlayerAttack playerAttack;
    public PlayerReceive playerReceive;

	private Animator anim;

	private bool playerMoving;
	private Vector2 lastMove;

    public void Init()
    {
        playerMovement.enabled = true;
        playerAttack.enabled = true;

        playerReceive.Init(playerStats);
    }

    void Start()
    {
		anim = GetComponent<Animator> ();
    }

	void Update () {
		
		if (!isLocalPlayer)
		{
			return;
		}
		/*
		SetAnim ();

		if (isServer)
			RpcSetAnim ();
		else
		{
			CmdSetAnim ();
		}
		*/
		CmdSetAnim ();
	}

	void SetAnim()
	{
		playerMoving = false;
		if (Input.GetAxisRaw ("Horizontal") != 0 || Input.GetAxisRaw ("Vertical") != 0) {
			playerMoving = true;
			lastMove = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		}
		anim.SetFloat ("MoveX", Input.GetAxisRaw ("Horizontal"));
		anim.SetFloat ("MoveY", Input.GetAxisRaw ("Vertical"));
		anim.SetBool ("PlayerMoving", playerMoving);
		anim.SetFloat ("LastMoveX", lastMove.x);
		anim.SetFloat ("LastMoveY", lastMove.y);
	}

	[Command]
	void CmdSetAnim()
	{
		playerMoving = false;
		if (Input.GetAxisRaw ("Horizontal") != 0 || Input.GetAxisRaw ("Vertical") != 0) {
			playerMoving = true;
			lastMove = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		}
		anim.SetFloat ("MoveX", Input.GetAxisRaw ("Horizontal"));
		anim.SetFloat ("MoveY", Input.GetAxisRaw ("Vertical"));
		anim.SetBool ("PlayerMoving", playerMoving);
		anim.SetFloat ("LastMoveX", lastMove.x);
		anim.SetFloat ("LastMoveY", lastMove.y);

		//RpcSetAnim ();
	}

	[ClientRpc]
	void RpcSetAnim()
	{
		if (!isServer)
		{
			playerMoving = false;
			if (Input.GetAxisRaw ("Horizontal") != 0 || Input.GetAxisRaw ("Vertical") != 0) {
				playerMoving = true;
				lastMove = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
			}
			anim.SetFloat ("MoveX", Input.GetAxisRaw ("Horizontal"));
			anim.SetFloat ("MoveY", Input.GetAxisRaw ("Vertical"));
			anim.SetBool ("PlayerMoving", playerMoving);
			anim.SetFloat ("LastMoveX", lastMove.x);
			anim.SetFloat ("LastMoveY", lastMove.y);
		}
	}

}
