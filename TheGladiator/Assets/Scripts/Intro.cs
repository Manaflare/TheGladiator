using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
    public void EndIntro()
    {
        MasterManager.ManagerLoadScene.LoadScene(1);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
