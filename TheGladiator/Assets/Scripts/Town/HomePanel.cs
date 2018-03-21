using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomePanel : MonoBehaviour {

    public GameObject sleepImage;
    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {

    }

    public void OnOK()
    {
        sleepImage.SetActive(true);
        TownManager.Instance.CloseCurrentWindow(true, CallBackEndSleep, 2.0f);
    }

    public void OnCancel()
    {
        TownManager.Instance.CloseCurrentWindow(false);
    }

    public void CallBackEndSleep()
    {
        sleepImage.SetActive(false);
        ListDataInfo playerDataInfo = MasterManager.ManagerGlobalData.GetPlayerDataInfo();
        //Generates stamina;
        //to do : depends on house level
        /*
            level 1 - 45%;
            level 2 - 65%;
            level 3 - 85%;
            level 4 - 110%;
         */
        playerDataInfo.statsList[0].Stamina = playerDataInfo.statsList[0].MaxStamina;
        //Refills HP to 100 %;
        playerDataInfo.statsList[0].HP = playerDataInfo.statsList[0].MAXHP;
        MasterManager.ManagerGlobalData.SavePlayerData();
        
    }
}
