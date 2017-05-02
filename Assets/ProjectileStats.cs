using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileStats : MonoBehaviour {

    public int damage;

    public void Init(int damage)
    {
        this.damage = damage;
    }
}
