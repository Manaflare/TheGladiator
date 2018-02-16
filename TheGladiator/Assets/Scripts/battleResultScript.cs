using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class battleResultScript : MonoBehaviour {
    public GameObject Player1;
    public GameObject Player2;
    public Text WinnerName;
    public Text LoserName;
	// Use this for initialization
	void Start () {

        Attribute player1Atrrib = Player1.GetComponent<Attribute>();
        Attribute player2Atrrib = Player2.GetComponent<Attribute>();
        if (player1Atrrib.getSTATS().HP > player2Atrrib.getSTATS().HP)
        {
            WinnerName.text = Utility.getStringFromName(player1Atrrib.getSTATS().PlayerType);
            LoserName.text = Utility.getStringFromName(player2Atrrib.getSTATS().PlayerType);
        }
        else 
        {
            WinnerName.text = Utility.getStringFromName(player2Atrrib.getSTATS().PlayerType);
            LoserName.text = Utility.getStringFromName(player1Atrrib.getSTATS().PlayerType);

        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
