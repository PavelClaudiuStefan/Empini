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
        if (isLocalPlayer)
        {
            playerMovement.enabled = true;
            playerMovement.Init(playerStats);
        }

        playerAttack.enabled = true;

        playerReceive.Init(playerStats);
        playerAttack.Init(playerStats);
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

        if (Input.GetKeyDown(KeyCode.Alpha1))
            playerStats.BulletSpeed++;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            playerStats.BulletDamage++;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            playerStats.MaxHealthIncreas();
        if (Input.GetKeyDown(KeyCode.Alpha4))
            playerStats.HealthRegen++;
        if (Input.GetKeyDown(KeyCode.Alpha5))
            playerStats.MovementSpeed++;

        /*SetAnim();

        if (isServer)
            RpcSetAnim();
        else
            CmdSetAnim();*/
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

		RpcSetAnim ();
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
