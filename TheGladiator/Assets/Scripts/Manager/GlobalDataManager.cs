using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataManager : MonoBehaviour, IManager {

    private ListStatus playerStatus;
    public void Initialize()
    {
        Debug.Log("boot Done " + typeof(GlobalDataManager));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SaveAllData()
    {
        //get data from player attribute and save it to json File
        Utility.WriteStatusToJSON(1, ref playerStatus);
    }

    public void LoadallData()
    {
        //get data from json and put it into player status
        playerStatus = Utility.ReadStatusFromJSON(1);    
        
    }
}
