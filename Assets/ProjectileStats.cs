using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileStats : MonoBehaviour {

    public PlayerStats playerStats;
    public int damage;

    public void Init(int damage, PlayerStats stats)
    {
        this.damage = damage;
        this.playerStats = stats;

    }
}
