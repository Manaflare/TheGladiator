using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class battleResultScript : MonoBehaviour {
    public GameObject Player1;
    public GameObject Player2;

    public Image winnerSprite;
    public Image loserSprite;

    public Text[] winnerStats;
    public Text[] loserStats;

    public Text WinnerName;
    public Text LoserName;
	// Use this for initialization

    void setStatValues(Text[] arrayVals, Stats s)
    {
        arrayVals[0].text = s.Strength.ToString();
        arrayVals[1].text = s.Dexterity.ToString();
        arrayVals[2].text = s.Agility.ToString();
        arrayVals[3].text = s.HP.ToString();
    }
	void Start () {

        Attribute player1Atrrib = Player1.GetComponent<Attribute>();
        Attribute player2Atrrib = Player2.GetComponent<Attribute>();
        if (player1Atrrib.getSTATS().HP > player2Atrrib.getSTATS().HP)
        {
            WinnerName.text = Utility.getStringFromName(player1Atrrib.getSTATS().playerType);
            winnerSprite.sprite = Player1.GetComponent<Image>().sprite;
            loserSprite.sprite = Player2.GetComponent<Image>().sprite;
            setStatValues(winnerStats, player1Atrrib.getSTATS());
            setStatValues(loserStats, player2Atrrib.getSTATS());
            LoserName.text = Utility.getStringFromName(player2Atrrib.getSTATS().playerType);
        }
        else 
        {
            WinnerName.text = Utility.getStringFromName(player2Atrrib.getSTATS().playerType);
            winnerSprite.sprite = Player2.GetComponent<Image>().sprite;
            loserSprite.sprite = Player1.GetComponent<Image>().sprite;
            setStatValues(winnerStats, player2Atrrib.getSTATS());
            setStatValues(loserStats, player1Atrrib.getSTATS());
            LoserName.text = Utility.getStringFromName(player1Atrrib.getSTATS().playerType);

        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
