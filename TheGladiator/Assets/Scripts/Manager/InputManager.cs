﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour, IManager {

    // Use this for initialization
    public void Initialize()
    {

    }

    // Update is called once per frame
    void Update () {
		
	}

    public bool GetKeyDown(KeyCode code)
    {
#if UNITY_XBOXONE
                return false;
#endif
        return Input.GetKeyDown(code);  
    }
}
