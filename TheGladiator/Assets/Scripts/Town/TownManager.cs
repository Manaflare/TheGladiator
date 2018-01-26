using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownManager : MonoBehaviour {

    private static TownManager instance;
    public static TownManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<TownManager>();
                if(instance == null)
                {
                    GameObject container = new GameObject("TownManager");
                    instance = container.AddComponent<TownManager>();
                }
            }

            return instance;
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
