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
