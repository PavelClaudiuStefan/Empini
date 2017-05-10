using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : SpawnableObject
{
    public override void OnCollisionEnter2D(Collision2D col)
    {
        if(manager != null)
            manager.ObjectHitted(col, this);
    }
   
}
