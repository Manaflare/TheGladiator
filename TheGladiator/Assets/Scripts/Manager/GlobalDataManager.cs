using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataManager : MonoBehaviour, IManager {

	public void Initialize()
    {
        Debug.Log("boot Done " + typeof(GlobalDataManager));
        ListStatus testStatus = Utility.GetStatsFromJSON(1);
        Debug.Log("Stamina : " + testStatus.statusList[0].Stamina);
        Debug.Log("Stamina : " + testStatus.statusList[1].Stamina);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
