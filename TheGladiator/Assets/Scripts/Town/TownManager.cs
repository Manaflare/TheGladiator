﻿using System;
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
    public Text goldText;
    private int selectedIndex = 0;
    private long gold;
	// Use this for initialization
	void Start ()
    {
        gold = MasterManager.ManagerGlobalData.GetEnvData().gold;
        Objects[selectedIndex].GetComponent<GlowButton>().StartGlow();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Objects[selectedIndex].GetComponent<GlowButton>().EndGlow();

        if (MasterManager.ManagerInput.GetKeyDown(KeyCode.LeftArrow))
        {
            DecideSelectedObject(-1);
        }
        else if (MasterManager.ManagerInput.GetKeyDown(KeyCode.RightArrow))
        {
            DecideSelectedObject(1);

        }
        else if (MasterManager.ManagerInput.GetKeyDown(KeyCode.UpArrow))
        {
            DecideSelectedObject(-3);


        }
        else if (MasterManager.ManagerInput.GetKeyDown(KeyCode.DownArrow))
        {
            DecideSelectedObject(3);
        }


        Objects[selectedIndex].GetComponent<GlowButton>().StartGlow();

        if (MasterManager.ManagerInput.GetKeyDown(KeyCode.Return))
        {
            if (selectedIndex == Panels.Length)
            {
                Debug.Log(Constants.DayTimeType.MORNING.ToString());
            }
            else
            {
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
                        if(buildingPanel.CheckStatus())
                        {
                            string panelName = Panels[selectedIndex].name;
                            StringBuilder s = new StringBuilder(panelName);
                            s.Replace("Panel", "");
                            MasterManager.ManagerLoadScene.LoadScene(panelName);
                        }
                        break;
                    default:
                        break;
                }
                
            }

        }

        if(MasterManager.ManagerInput.GetKeyDown(KeyCode.E))
        {
            object[] test = { "MainMenu" };
            MasterManager.ManagerPopup.ShowMessageBox("TEST", "This is a test", Constants.PopupType.POPUP_NO, TEST, test);
            //for rest button
        }


        UpdateUI();
    }

    private void UpdateUI()
    {
        //"1,234,567"
        goldText.text = gold.ToString("N0");
    }

    public void CloseCurrentWindow(bool bSpendTime = true, Constants.CallbackFunction callFunc = null, float spendingTurn = 1.0f)
    {
        MasterManager.ManagerGlobalData.SavePlayerData();
        Panels[selectedIndex].SetActive(false);

        if (bSpendTime)
            DayNightCycleManager.Instance.SpendTime(spendingTurn, callFunc);
    }

    //exmaple code
    public void TEST(object[] asd)
    {
        MasterManager.ManagerLoadScene.LoadScene(asd[0].ToString());
            
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
