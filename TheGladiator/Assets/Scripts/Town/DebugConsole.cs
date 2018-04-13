using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugConsole : MonoBehaviour {

 	// Use this for initialization
	void Start ()
    {
        Debug.Log("DebugConsole");
    }
	
	// Update is called once per frame
	void Update ()
    {
		
       
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 100), "I am a button"))
            print("You clicked the button!");

    }

    public void OnOpenDebugConsole()
    {
        Debug.Log("AasdSD");
    }

}
