using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSet : MonoBehaviour {
    public GameObject[] sets;

	public void SetSet(int idx) // =)) dar are logica 
    {
        for (int i = 0; i < sets.Length; i++)
            if (i < idx)
                sets[i].SetActive(true);
            else
                sets[i].SetActive(false);

        // Sunt lenes , n-am chef sa fac ceva mai sofisticat 
    }

}
