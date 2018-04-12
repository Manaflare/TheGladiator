using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Stats s = MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0];
		foreach (var a in GetComponentsInChildren<Text>())
        {
            if (a.name == "HP")
            {
                a. text = s.HP.ToString();
            }
            else if (a.name == "STR")
            {
                a.text = s.Strength.ToString();
            }
            else if (a.name == "AGI")
            {
                a.text = s.Agility.ToString();
            }
            else if (a.name == "DEX")
            {
                a.text = s.Dexterity.ToString();
            }
            else if (a.name == "STAM")
            {
                a.text = s.Stamina.ToString();
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
