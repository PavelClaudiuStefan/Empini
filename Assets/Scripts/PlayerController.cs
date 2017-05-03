using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    public PlayerStats playerStats;
    public PlayerMovement playerMovement;
    public PlayerAttack playerAttack;
    public PlayerReceive playerReceive;

	

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
    }
}
