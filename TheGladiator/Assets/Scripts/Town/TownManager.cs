using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                        Panels[selectedIndex].SetActive(true);
                        break;
                    case Constants.BuildingPanel_Type.SCENE:
                        string panelName = Panels[selectedIndex].name;
                        StringBuilder s = new StringBuilder(panelName);
                        s.Replace("Panel", "");
                        MasterManager.ManagerLoadScene.LoadScene(panelName);
                        break;
                    default:
                        break;
                }
                
            }

        }

        if(MasterManager.ManagerInput.GetKeyDown(KeyCode.E))
        {
            object[] test = { 1, true, "ASD", 4, 5.7f };
            MasterManager.ManagerPopup.ShowMessageBox("TEST", "This is a test", Constants.PopupType.POPUP_NO, TEST, test);
            //for rest button
        }
    }

    public void CloseCurrentWindow()
    {
        MasterManager.ManagerGlobalData.SavePlayerData();
        Panels[selectedIndex].SetActive(false);
        DayNightCycleManager.Instance.SpendTime();
    }

    //exmaple code
    private void TEST(object[] asd)
    {
        foreach(object v in asd)
        {
            Debug.Log("Elements : " + v);
        }
            
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
