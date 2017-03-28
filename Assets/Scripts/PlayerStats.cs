using UnityEngine;
using UnityEngine.Networking;

public class PlayerStats : NetworkBehaviour {

    [SyncVar]
    int playerHp = 100;

    public TextMesh hpBar;

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

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            PlayerHp--;
            CmdDestroyBullet(col.gameObject);
        }
    }

    [Command]
    void CmdDestroyBullet(GameObject bullet)
    {
        NetworkServer.Destroy(bullet);
    }

}
