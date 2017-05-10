using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RegisterPlayer : GameEvent{
}

public class RoundStarter : NetworkBehaviour
{
    public int playerToBe;

    private int players;

    public bool ready;

    void Start()
    {
        if (isServer)
        {
            EventManager.instance.AddListener<RegisterPlayer>(RegisterPlayer);
            StartCoroutine(WaitForPlayers());
        }

    }
    void RegisterPlayer(RegisterPlayer e)
    {
        players++;
    }

    IEnumerator WaitForPlayers()
    {
        while (players < playerToBe)
            yield return null;

        ready = true;
    }

}
