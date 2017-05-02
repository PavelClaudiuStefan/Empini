using UnityEngine;
using UnityEngine.Networking;

public class PlayerStats : NetworkBehaviour {

    public TextMesh hpBar;

    private int currentPoints = 10;

    private int playerHp = 100;
    private float bulletSpeed = 5;
    private int bulletDamage = 1;

    public int PlayerHp
    {
        get { return playerHp; }
        set
        {
            playerHp = value;
            hpBar.text = playerHp.ToString();
            if (isLocalPlayer)
                EventManager.instance.Raise(new SetHpUiEvent(playerHp));

            if (isServer)
                RpcPlayerHp(value);
            else
                CmdPlayerHp(value);
        }
    }

    public float BulletSpeed
    {
        get { return bulletSpeed; }
        set {
            bulletSpeed = value;

            if (isServer)
                RpcBulletSpeed(value);
            else
                CmdBulletSpeed(value);
        }
    }
    public int BulletDamage
    {
        get { return bulletDamage; }
        set {
            bulletDamage = value;

            if (isServer)
                RpcBulletDamage(value);
            else
                CmdBulletDamage(value);
        }
    }

    [Command]
    void CmdPlayerHp(int value)
    {
        playerHp = value;
        hpBar.text = playerHp.ToString();
        if (isLocalPlayer)
            EventManager.instance.Raise(new SetHpUiEvent(playerHp));

        RpcPlayerHp(value);
    }
    [ClientRpc]
    void RpcPlayerHp(int value)
    {
        if(!isServer)
        {
            playerHp = value;
            hpBar.text = playerHp.ToString();
            if (isLocalPlayer)
                EventManager.instance.Raise(new SetHpUiEvent(playerHp));
        }
    }


    [Command]
    void CmdBulletSpeed(float value)
    {
        bulletSpeed = value;

        RpcBulletSpeed(value);
    }
    [ClientRpc]
    void RpcBulletSpeed(float value)
    {
        if (!isServer)
        {
            bulletSpeed = value;
        }
    }

    [Command]
    void CmdBulletDamage(int value)
    {
        bulletDamage = value;

        RpcBulletDamage(value);
    }

    [ClientRpc]
    void RpcBulletDamage(int value)
    {
        if (!isServer)
        {
            bulletDamage = value;
        }
    }


}
