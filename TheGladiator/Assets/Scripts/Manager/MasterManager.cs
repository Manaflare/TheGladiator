using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Configuration
{
    public string lang = "en_ca";
    public bool hasReadTutorial = false;
 }

[RequireComponent(typeof(LoadSceneManager))]
[RequireComponent(typeof(GlobalDataManager))]
[RequireComponent(typeof(SoundManager))]
[RequireComponent(typeof(SpriteManager))]
[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(PopupManager))]
[RequireComponent(typeof(LocalizeManager))]
public class MasterManager : MonoBehaviour {

    private List<IManager> managerList = new List<IManager>();

    public static LoadSceneManager  ManagerLoadScene    { get; private set; }
    public static GlobalDataManager ManagerGlobalData   { get; private set; }
    public static SoundManager      ManagerSound        { get; private set; }
    public static SpriteManager      ManagerSprite      { get; private set; }

    public static PopupManager       ManagerPopup       { get; private set; }
    public static LocalizeManager    ManagerLocalize       { get; private set; }
    public static InputManager ManagerInput { get; private set; }

    private static int instance_index = 0;
    private void Awake()
    {
        if(instance_index > 0)
        {
            return;
        }

        MasterManager instance = GameObject.FindObjectOfType<MasterManager>();
        if(instance != null)
        {
            
            instance_index++;
        }

        

        QualitySettings.vSyncCount = 0;
        ManagerLoadScene = GetComponent<LoadSceneManager>();
        ManagerGlobalData = GetComponent<GlobalDataManager>();
        ManagerSound = GetComponent<SoundManager>();
        ManagerInput = GetComponent<InputManager>();
        ManagerSprite = GetComponent<SpriteManager>();
        ManagerPopup = GetComponent<PopupManager>();
        ManagerLocalize = GetComponent<LocalizeManager>();

        //added all manager in the manager list
        managerList.Add(ManagerLoadScene);
        managerList.Add(ManagerGlobalData);
        managerList.Add(ManagerSound);
        managerList.Add(ManagerSprite);
        managerList.Add(ManagerInput);
        managerList.Add(ManagerPopup);
        managerList.Add(ManagerLocalize);
        StartCoroutine(IE_BootAllManager());

        string path = Application.dataPath;
        //If mobileCreate a copy of the data json
        if (Application.platform == RuntimePlatform.Switch || 
            Application.platform == RuntimePlatform.Android ||
               Application.platform == RuntimePlatform.IPhonePlayer)
        {
            path = Application.persistentDataPath;
        }else
        {
            path = Application.dataPath;
        }
            
        TextAsset _jsonString = (TextAsset)Resources.Load("JSON/EnemyData", typeof(TextAsset));
        System.IO.File.WriteAllText(path + Utility.JsonFileList[Constants.JSONIndex.DATA_ENEMY], _jsonString.text);

        _jsonString = (TextAsset)Resources.Load("JSON/ItemData", typeof(TextAsset));
        System.IO.File.WriteAllText(path + Utility.JsonFileList[Constants.JSONIndex.DATA_ITEM], _jsonString.text);

        _jsonString = (TextAsset)Resources.Load("JSON/WorkData", typeof(TextAsset));
        System.IO.File.WriteAllText(path + Utility.JsonFileList[Constants.JSONIndex.DATA_WORK], _jsonString.text);

        _jsonString = (TextAsset)Resources.Load("JSON/Config", typeof(TextAsset));
        System.IO.File.WriteAllText(path + Utility.JsonFileList[Constants.JSONIndex.DATA_CONFIG], _jsonString.text);

        _jsonString = (TextAsset)Resources.Load("JSON/Credit", typeof(TextAsset));
        System.IO.File.WriteAllText(path + Utility.JsonFileList[Constants.JSONIndex.DATA_CREDIT], _jsonString.text);

        ManagerGlobalData.LoadallData();

        //keep this gameobject the entire project
        DontDestroyOnLoad(gameObject);
    }

    IEnumerator IE_BootAllManager()
    {
        foreach(IManager manager in managerList)
        {
            manager.Initialize();
        }

        yield return null;
    }

    void OnApplicationQuit()
    {
        Debug.Log("Application ending after " + Time.time.ToString("0.0") + " seconds");
        ManagerGlobalData.SaveAllData();
    }
}
