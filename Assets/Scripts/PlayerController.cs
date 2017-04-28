using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    public PlayerStats playerStats;
    public PlayerMovement playerMovement;
    public PlayerAttack playerAttack;
    public PlayerReceive playerReceive;

    public void Init()
    {
        playerMovement.enabled = true;
        playerAttack.enabled = true;

        playerReceive.Init(playerStats);
    }

    void Start()
    {

    }

	void Update () {
        
	}

}
