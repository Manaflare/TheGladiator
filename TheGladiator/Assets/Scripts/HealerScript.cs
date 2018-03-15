using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealerScript : MonoBehaviour
{

    public GameObject buttonLow;
    public GameObject buttonMid;
    public GameObject buttonHigh;
    public GameObject leaveButton;

	// Use this for initialization
	void Start ()
    {
		ListDataInfo playerData = MasterManager.ManagerGlobalData.GetPlayerDataInfo();
        //buttons are only usable if your tier is high enough; better healing options require you to be higher tier
        if (playerData.playerTier == 1)
        {
            buttonLow.GetComponent<Button>().interactable = true;
            buttonMid.GetComponent<Button>().interactable = false;
            buttonHigh.GetComponent<Button>().interactable = false;
        }
        else if (playerData.playerTier == 2)
        {
            buttonLow.GetComponent<Button>().interactable = true;
            buttonMid.GetComponent<Button>().interactable = true;
            buttonHigh.GetComponent<Button>().interactable = false;
        }
        else if (playerData.playerTier == 3)
        {
            buttonLow.GetComponent<Button>().interactable = true;
            buttonMid.GetComponent<Button>().interactable = true;
            buttonHigh.GetComponent<Button>().interactable = true;
        }

        Button btn = leaveButton.GetComponent<Button>();
        btn.onClick.AddListener(DestroyOnClick);
    }

    //closes the healer's page
    public void DestroyOnClick()
    {
        Destroy(this.transform.root.gameObject);
    }
}
