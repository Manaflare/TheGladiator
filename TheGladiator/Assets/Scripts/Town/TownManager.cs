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
	void Start () {
        Objects[selectedIndex].GetComponent<GlowButton>().StartGlow();
    }
	
	// Update is called once per frame
	void Update () {
		if(MasterManager.ManagerInput.GetKeyDown(KeyCode.UpArrow))
        {
            Objects[selectedIndex].GetComponent<GlowButton>().EndGlow();
            ++selectedIndex;
            selectedIndex = Mathf.Min(selectedIndex, Objects.Length - 1);
            Objects[selectedIndex].GetComponent<GlowButton>().StartGlow();
        }
        else if (MasterManager.ManagerInput.GetKeyDown(KeyCode.DownArrow))
        {
            Objects[selectedIndex].GetComponent<GlowButton>().EndGlow();
            --selectedIndex;
            selectedIndex = Mathf.Max(0, selectedIndex);
            Objects[selectedIndex].GetComponent<GlowButton>().StartGlow();
        }

        if(MasterManager.ManagerInput.GetKeyDown(KeyCode.Return))
        {
            Panels[selectedIndex].SetActive(true);
        }
    }
}
