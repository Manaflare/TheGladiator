using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

    ListDataInfo playerData;

    // Use this for initialization
    void Start () {

        playerData = MasterManager.ManagerGlobalData.GetPlayerDataInfo();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
