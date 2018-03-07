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

	// Use this for initialization
	void Start ()
    {
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
                Panels[selectedIndex].SetActive(true);
            }

        }

        if(MasterManager.ManagerInput.GetKeyDown(KeyCode.E))
        {
            MasterManager.ManagerPopup.ShowMessageBox("TEST", "This is a test", Constants.PopupType.POPUP_NO);
            //for rest button
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
