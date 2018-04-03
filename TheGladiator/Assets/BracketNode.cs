using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BracketNode : MonoBehaviour {

    
	// Use this for initialization
	void Start () {
        this.GetComponentInChildren<BracketCharacterDisplay>().Draw(MasterManager.ManagerGlobalData.GetPlayerDataInfo());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
