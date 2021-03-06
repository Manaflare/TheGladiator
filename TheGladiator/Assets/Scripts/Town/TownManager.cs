﻿using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TownManager : MonoBehaviour
{

    private static TownManager instance;
    public static TownManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<TownManager>();
                if (instance == null)
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
    private bool FirstTownScene = false; // this town manager is instantiated first after tutorial
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

    public Text Tier;
    public Slider HPBar;
    public Text barMaxHP;
    public Text barCurHP;
    public Slider StaminaBar;
    public Text barMaxSTA;
    public Text barCurSTA;

    // declare variable for BGM
    public AudioClip backgroundMusic;

    public Text MaxSTA;

    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.GetInt("WinBracket") == 1)
        {
            //call popup window
            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                {
                    Panels[11].SetActive(true);
                }
                if (i == 1)
                {
                    Panels[12].SetActive(true);
                }
            }
            PlayerPrefs.SetInt("WinBracket", 0);
        }
        // call BGM
        MasterManager.ManagerSound.PlayBackgroundMusic(backgroundMusic);
        if (!MasterManager.ManagerGlobalData.GetConfiguration().hasReadTutorial)
        {
            Panels[9].SetActive(true);
        }

        gold = MasterManager.ManagerGlobalData.GetEnvData().gold;
        //Objects[selectedIndex].GetComponent<GlowButton>().StartGlow();
        UpdatePlayerUI();

        if (MasterManager.ManagerGlobalData.GetPlayerDataInfo().playerTier > Constants.MAX_ENEMY_RANK)
        {
            Panels[10].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (MasterManager.ManagerInput.GetKeyDown(KeyCode.Escape))
        {
            SelectPanel(2);
        }
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



        if (MasterManager.ManagerInput.GetKeyDown(KeyCode.E))
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
                string panelName = Panels[selectedIndex].name;
                StringBuilder s = new StringBuilder(panelName);
                s.Replace("Panel", "");

                if (buildingPanel.GetStatus() == Constants.BuildingPanel_Status.ONLY_SUNDAY
                    && MasterManager.ManagerGlobalData.GetEnvData().days != Constants.DayType.SUNDAY)
                {

                    Panels[selectedIndex].SetActive(true);
                    Panels[selectedIndex].GetComponentInChildren<ScenePanel>().SetSceneName(s.ToString());

                }
                else
                {
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

    public void CloseCurrentWindow(bool bSpendTime = true, Constants.CallbackFunction callFunc = null, float spendingTurn = 1.0f, Constants.ClockImageType clockType = Constants.ClockImageType.HOUR_GLASS)
    {
        MasterManager.ManagerGlobalData.SavePlayerData();
        Panels[selectedIndex].SetActive(false);

        if (bSpendTime)
            DayNightCycleManager.Instance.SpendTime(spendingTurn, callFunc, clockType);

        UpdatePlayerUI();


    }

    public void SetSelectPanel(int index)
    {
        selectedIndex = index;
    }

    public void UpdatePlayerUI()
    {
        ListDataInfo playerData = MasterManager.ManagerGlobalData.GetPlayerDataInfo();
        Stats actStat = playerData.GetActualStats();
        playerName.text = playerData.statsList[0].Name;
        MaxHP.text = (actStat.MAXHP * Constants.HP_MULTIPLIER).ToString();
        // HP.text = actStat.HP.ToString();
        STR.text = actStat.Strength.ToString();
        AGI.text = actStat.Agility.ToString();
        DEX.text = actStat.Dexterity.ToString();
        // STA.text = actStat.Stamina.ToString();
        MaxSTA.text = actStat.MaxStamina.ToString();
        Tier.text = playerData.playerTier.ToString();
        Character.GetComponent<CharacterSpriteManager>().UpdateSprites();

        if (actStat.HP > actStat.MAXHP)
        {
            actStat.HP = (int)(actStat.MAXHP * Constants.HP_MULTIPLIER);
            playerData.statsList[0].HP = actStat.HP;
            MasterManager.ManagerGlobalData.SavePlayerData();
        }

        barMaxHP.text = MaxHP.text;
        barCurHP.text = actStat.HP.ToString();
        HPBar.maxValue = actStat.MAXHP;
        HPBar.minValue = 1;
        HPBar.value = actStat.HP;

        barMaxSTA.text = MaxSTA.text;
        barCurSTA.text = actStat.Stamina.ToString();
        StaminaBar.maxValue = actStat.MaxStamina;
        StaminaBar.minValue = 1;
        StaminaBar.value = actStat.Stamina;


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

    public void SetupAllAfterTutorial()
    {
        FirstTownScene = true;
    }

    public bool IsitFirstPlay()
    {
        return FirstTownScene;
    }

    public void OnGoBackToMainMenu()
    {
        MasterManager.ManagerLoadScene.LoadScene("MainMenu");
    }
}
