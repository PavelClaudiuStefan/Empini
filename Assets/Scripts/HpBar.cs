using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour {

    public Renderer bar;

    public void SetBar(float current, float max)
    {
        bar.material.SetFloat("_Progress", (float)current/max);
    }


}
