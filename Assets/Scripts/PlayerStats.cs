using UnityEngine;
using UnityEngine.Networking;

public class PlayerStats : NetworkBehaviour {

    public int playerHp = 100;

    public TextMesh hpBar;

    private PlayerInventory playerInventory;

    public int PlayerHp
    {
        get { return playerHp; }
        set
        {
            playerHp = value;
            hpBar.text = playerHp.ToString();
            if (isLocalPlayer)
                EventManager.instance.Raise(new SetHpUiEvent(playerHp));
        }
    }


    public void GiveWood(int wood)
    {
        playerInventory.AddWood(wood);
    }
    public void GetWood(int wood)
    {
        playerInventory.ExtractWood(wood);
    }

    void Start()
    {
        playerInventory = new PlayerInventory();
    }  

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            PlayerHp--;
            Destroy(col.gameObject);
        }
    }

}
