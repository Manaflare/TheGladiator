using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour, IManager {

    // Use this for initialization
    private bool afterBattle = false;
    public bool AfterBattle
    {
        get { return afterBattle; }
        set { afterBattle = value; }
    }

    public void Initialize()
    {
        afterBattle = false;
    }

    // Update is called once per frame
    void Update () {
		
	}
    public void LoadScene(string sceneName, bool withLoading = true)
    {
        if(withLoading)
        {
            LoadSceneAsync.LoadScene(sceneName);
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }

        if (SceneManager.GetActiveScene().name == "Arena")
        {
            afterBattle = true;
        }
    
        MasterManager.ManagerGlobalData.SaveAllData();

    }

    public void LoadScene(int sceneIndex, bool withLoading = true)
    {
        string scene_path = SceneUtility.GetScenePathByBuildIndex(sceneIndex);
        string scene_name_with_extension = scene_path.Split('/')[2];
        string scene_name = System.IO.Path.GetFileNameWithoutExtension(scene_name_with_extension);
        LoadScene(scene_name, withLoading);
    }
}
