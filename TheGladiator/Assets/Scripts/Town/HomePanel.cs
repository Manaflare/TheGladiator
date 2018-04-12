using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomePanel : MonoBehaviour
{

    public GameObject sleepImage;
    public Text houseType;
    public Text data;
    public Text cost;
    public Image coin;
    public Button upgradeBtn;
    int BaseCost = 150;
    int FinalCost;

    // Use this for initialization
    void Start()
    {

        FinalCost = (int)(BaseCost * Mathf.Pow(MasterManager.ManagerGlobalData.GetEnvData().house_level, 3));
        cost.text = FinalCost.ToString();

        Constants.HouseType houseType = (Constants.HouseType)MasterManager.ManagerGlobalData.GetEnvData().house_level;
        string dataText = "";
        switch (houseType)
        {
            case Constants.HouseType.SMALL:
                this.houseType.text = "Small House";
                dataText = "45%\n45%";
                break;
            case Constants.HouseType.MEDIUM:
                dataText = "65%\n65%";
                this.houseType.text = "Medium House";
                break;
            case Constants.HouseType.HUGE:
                dataText = "85%\n85%";
                this.houseType.text = "Huge House";
                break;
            case Constants.HouseType.MANSION:
                this.houseType.text = "Mansion";
                dataText = "100%\n100%";
                upgradeBtn.gameObject.SetActive(false);
                coin.gameObject.SetActive(false);
                cost.gameObject.SetActive(false);
                break;
            default:
                break;
        }
        data.text = dataText;
    }
    private void OnEnable()
    {
        Start();
    }
    public void UpgradeHome()
    {
        if (MasterManager.ManagerGlobalData.GetEnvData().gold > FinalCost)
        {
            MasterManager.ManagerGlobalData.GetEnvData().gold -= FinalCost;
            MasterManager.ManagerGlobalData.GetEnvData().house_level++;
            MasterManager.ManagerGlobalData.SaveEnvData();
            TownManager.Instance.UpdatePlayerUI();
            Start();
        }
        else
        {
            MasterManager.ManagerPopup.ShowMessageBox("Oh No!", "You don't have enought gold to buy this house", Constants.PopupType.POPUP_NO);
        }
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
                multiPlier = 1.00f;
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
