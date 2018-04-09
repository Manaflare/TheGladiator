using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    public GameObject[] steps;
    int currentItem;
    public string nextScene;
    public bool isTutorial;
    Configuration config;
    // Use this for initialization
    void Start()
    {

        config = MasterManager.ManagerGlobalData.GetConfiguration();
        if (isTutorial && config.hasReadTutorial)
        {
            this.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            this.transform.parent.gameObject.SetActive(true);
            currentItem = 0;
        }
    }

    public void NextItem()
    {
        if (currentItem == steps.Length - 1)
        {
            if (nextScene != null && nextScene != "")
            {
                MasterManager.ManagerLoadScene.LoadScene(nextScene,false);
            }
            if (isTutorial)
            {
                config.hasReadTutorial = true;
                MasterManager.ManagerGlobalData.SaveConfig();
            }
            this.transform.parent.gameObject.SetActive(false);
            return;
        }
        steps[currentItem].SetActive(false);
        currentItem++;
        steps[currentItem].SetActive(true);
    }

}
