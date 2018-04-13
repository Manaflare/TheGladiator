using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankUpScript : MonoBehaviour {

    public Text currentTier;

    public Button continueButton;

	// Use this for initialization
	void Start ()
    {
        Button btn = continueButton.GetComponent<Button>();
        btn.onClick.AddListener(closeOnClick);

        ListDataInfo playerData = MasterManager.ManagerGlobalData.GetPlayerDataInfo();
        currentTier.text = playerData.playerTier.ToString();
    }
	
	public void closeOnClick()
    {
        this.transform.root.gameObject.SetActive(false);
    }
}
