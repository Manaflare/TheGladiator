using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayButtonSound(string soundname)
    {
        MasterManager.ManagerSound.PlaySingleSound(soundname);
    }
}
