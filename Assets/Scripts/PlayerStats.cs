using UnityEngine;
using UnityEngine.Networking;

public class PlayerStats : NetworkBehaviour {

    public int playerHp = 100;

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

            if (isServer)
                RpcPlayerHp(value);
            else
                CmdPlayerHp(value);
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


}
