using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour {

    private List<GameObject> models;

    public void start()
    {
        models = new List<GameObject>();
        foreach(Transform t in transform)
        {
            models.Add(t.gameObject);
            t.gameObject.SetActive(false);
            Debug.Log("test");
        }
    }
}
