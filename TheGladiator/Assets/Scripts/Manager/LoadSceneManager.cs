using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour, IManager {

    // Use this for initialization
    public void Initialize()
    {

    }

    // Update is called once per frame
    void Update () {
		
	}
    public void LoadScene(string sceneName)
    {
        Scene curScene = SceneManager.GetSceneByName(sceneName);
        LoadScene(curScene.buildIndex);
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
        MasterManager.ManagerGlobalData.SaveAllData();
    }
}
