using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataManager : MonoBehaviour, IManager {

	public void Initialize()
    {
        Debug.Log("boot Done " + typeof(GlobalDataManager));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
