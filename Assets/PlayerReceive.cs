using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReceive : MonoBehaviour
{
    private PlayerStats playerStats;

    public void Init(PlayerStats playerStats)
    {
        enabled = true;
        this.playerStats = playerStats;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            if (enabled)
                   playerStats.PlayerHp--;
            Destroy(col.gameObject);
        }
        
    }

    void Update()
    {

    }
}
