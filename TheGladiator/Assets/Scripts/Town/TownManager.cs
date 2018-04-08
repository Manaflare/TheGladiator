using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TownManager : MonoBehaviour {

    private static TownManager instance;
    public static TownManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<TownManager>();
                if(instance == null)
                {
                    GameObject container = new GameObject("TownManager");
                    instance = container.AddComponent<TownManager>();
                }
            }

            return instance;
        }
    }


    public GameObject[] Objects;
    public GameObject[] Panels;
   
    private int selectedIndex = 0;
    private long gold;

    [Header("Player Display")]
    public GameObject Character;
    public Text playerName;
    public Text MaxHP;
    public Text HP;
    public Text STR;
    public Text AGI;
    public Text DEX;
    public Text STA;
    public Text goldText;

    // declare variable for BGM
    public AudioClip backgroundMusic;

    public Text MaxSTA;

    // Use this for initialization
    void Start ()
    {
        // call BGM
        MasterManager.ManagerSound.PlayBackgroundMusic(backgroundMusic);
        if (!MasterManager.ManagerGlobalData.GetConfiguration().hasReadTutorial)
        {
            Panels[9].SetActive(true);
        }

        gold = MasterManager.ManagerGlobalData.GetEnvData().gold;
        //Objects[selectedIndex].GetComponent<GlowButton>().StartGlow();
        UpdatePlayerUI();

        if (PlayerPrefs.GetInt("Ending", 0) == 1)
        {
            Panels[10].SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Objects[selectedIndex].GetComponent<GlowButton>().EndGlow();

        //if (MasterManager.ManagerInput.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    DecideSelectedObject(-1);
        //}
        //else if (MasterManager.ManagerInput.GetKeyDown(KeyCode.RightArrow))
        //{
        //    DecideSelectedObject(1);

        //}
        //else if (MasterManager.ManagerInput.GetKeyDown(KeyCode.UpArrow))
        //{
        //    DecideSelectedObject(-3);


        //}
        //else if (MasterManager.ManagerInput.GetKeyDown(KeyCode.DownArrow))
        //{
        //    DecideSelectedObject(3);
        //}


        //Objects[selectedIndex].GetComponent<GlowButton>().StartGlow();

        //if (MasterManager.ManagerInput.GetKeyDown(KeyCode.Return))
        //{
        //    if (selectedIndex == Panels.Length)
        //    {
        //        Debug.Log(Constants.DayTimeType.MORNING.ToString());
        //    }
        //    else
        //    {
        //        BuildingPanel buildingPanel = Panels[selectedIndex].GetComponent<BuildingPanel>();
        //        switch (buildingPanel.BuildingType)
        //        {
        //            case Constants.BuildingPanel_Type.NOT_IMPLEMENTED:
        //                MasterManager.ManagerPopup.ShowMessageBox("System", "Not implemented yet", Constants.PopupType.POPUP_NO);
        //                break;
        //            case Constants.BuildingPanel_Type.WINDOW:
        //                if (buildingPanel.CheckStatus())
        //                {
        //                    Panels[selectedIndex].SetActive(true);
        //                }
        //                break;
        //            case Constants.BuildingPanel_Type.SCENE:
        //                if(buildingPanel.CheckStatus())
        //                {
        //                    string panelName = Panels[selectedIndex].name;
        //                    StringBuilder s = new StringBuilder(panelName);
        //                    s.Replace("Panel", "");
        //                    MasterManager.ManagerLoadScene.LoadScene(panelName);
        //                }
        //                break;
        //            default:
        //                break;
        //        }
                
        //    }

        //}

       

        if(MasterManager.ManagerInput.GetKeyDown(KeyCode.E))
        {
            object[] test = { "MainMenu" };
            MasterManager.ManagerPopup.ShowMessageBox("TEST", "This is a test", Constants.PopupType.POPUP_NO, TEST, test);
            //for rest button
        }
    }

    public void SelectPanel(int index)
    {
        selectedIndex = index;
        BuildingPanel buildingPanel = Panels[selectedIndex].GetComponent<BuildingPanel>();
        switch (buildingPanel.BuildingType)
        {
            case Constants.BuildingPanel_Type.NOT_IMPLEMENTED:
                MasterManager.ManagerPopup.ShowMessageBox("System", "Not implemented yet", Constants.PopupType.POPUP_NO);
                break;
            case Constants.BuildingPanel_Type.WINDOW:
                if (buildingPanel.CheckStatus())
                {
                    Panels[selectedIndex].SetActive(true);
                }
                break;
            case Constants.BuildingPanel_Type.SCENE:
                if (buildingPanel.CheckStatus())
                {
                    string panelName = Panels[selectedIndex].name;
                    StringBuilder s = new StringBuilder(panelName);
                    s.Replace("Panel", "");
                    MasterManager.ManagerLoadScene.LoadScene(s.ToString());
                }
                break;
            default:
                break;
        }
    }

    

    public void GoBackToMusic()
    {
        MasterManager.ManagerSound.PlayBackgroundMusic(backgroundMusic);
    }

    public void CloseCurrentWindow(bool bSpendTime = true, Constants.CallbackFunction callFunc = null, float spendingTurn = 1.0f)
    {
        MasterManager.ManagerGlobalData.SavePlayerData();
        Panels[selectedIndex].SetActive(false);

        if (bSpendTime)
            DayNightCycleManager.Instance.SpendTime(spendingTurn, callFunc);

        UpdatePlayerUI();

        
    }

    public void UpdatePlayerUI()
    {
        ListDataInfo playerData = MasterManager.ManagerGlobalData.GetPlayerDataInfo();
        Stats actStat = playerData.GetActualStats();
        playerName.text = playerData.statsList[0].Name;
        MaxHP.text = (actStat.MAXHP * Constants.HP_MULTIPLIER).ToString();
        HP.text = actStat.HP.ToString();
        STR.text = actStat.Strength.ToString();
        AGI.text = actStat.Agility.ToString();
        DEX.text = actStat.Dexterity.ToString();
        STA.text = actStat.Stamina.ToString();
        MaxSTA.text = actStat.MaxStamina.ToString();

        Character.GetComponent<CharacterSpriteManager>().UpdateSprites();

        UpdateEnvUI();
    }

    private void UpdateEnvUI()
    {
        //"1,234,567"
        goldText.text = MasterManager.ManagerGlobalData.GetEnvData().gold.ToString("N0");
    }
    //exmaple code
    public void TEST(object[] asd)
    {
        MasterManager.ManagerLoadScene.LoadScene(asd[0].ToString());
            
    }

    public void WorkForNextWeek()
    {
        //work panel
        Panels[0].GetComponentInChildren<WorkPanel>().ResetWork();
    }

    void DecideSelectedObject(int increment)
    {
        /*
            0, 0, 0,
            0, 0, 0,
            0, 0, 0,   
            
            Array of buildings.
            index of the array should be increased by 1 or 3 depends one what keyboard is pressed
            i.e) uparrow or down arrow +- 3  left or right arrow +- 1
         
         */
        int oldIndex = selectedIndex;

        do
        {
            selectedIndex += increment;
            //check if it is out of index
            if (selectedIndex < 0 || selectedIndex > Objects.Length - 1)
            {
                selectedIndex = oldIndex;
                break;
            }
        }
        while (Objects[selectedIndex] == null);

        //check if it's still null
        if (Objects[selectedIndex] == null)
            selectedIndex = oldIndex;
    }
}
