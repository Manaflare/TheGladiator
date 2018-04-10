using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomePanel : MonoBehaviour
{

    public GameObject sleepImage;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnOK()
    {
        //  sleepImage.SetActive(true);
        TownManager.Instance.CloseCurrentWindow(true, CallBackEndSleep, 1.0f);
    }

    public void OnCancel()
    {
        TownManager.Instance.CloseCurrentWindow(false);
    }

    public void CallBackEndSleep()
    {
        // sleepImage.SetActive(false);
        ListDataInfo playerDataInfo = MasterManager.ManagerGlobalData.GetPlayerDataInfo();
        EnvironmentData envData = MasterManager.ManagerGlobalData.GetEnvData();
        Constants.HouseType houseType = (Constants.HouseType)envData.house_level;
        float multiPlier = 100.0f;
        //Generates stamina;
        //to do : depends on house level
        /*
            level 1 - 45%;
            level 2 - 65%;
            level 3 - 85%;
            level 4 - 110%;
         */
        switch (houseType)
        {
            case Constants.HouseType.SMALL:
                multiPlier = 0.45f;
                break;
            case Constants.HouseType.MEDIUM:
                multiPlier = 0.65f;
                break;
            case Constants.HouseType.HUGE:
                multiPlier = 0.85f;
                break;
            case Constants.HouseType.MANSION:
                multiPlier = 1.10f;
                break;
            default:
                break;
        }
        short addAmount = (short)(playerDataInfo.GetActualStats().MaxStamina * multiPlier);
        playerDataInfo.statsList[0].Stamina = (short)Mathf.Min((int)playerDataInfo.statsList[0].MaxStamina, (int)playerDataInfo.statsList[0].Stamina + addAmount);
        //Refills HP to 100 %;
        playerDataInfo.statsList[0].HP = (int)(playerDataInfo.GetActualStats().MAXHP * Constants.HP_MULTIPLIER);
        MasterManager.ManagerGlobalData.SavePlayerData();
        TownManager.Instance.UpdatePlayerUI();

    }
}
