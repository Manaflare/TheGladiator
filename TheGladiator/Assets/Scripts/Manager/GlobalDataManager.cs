using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataManager : MonoBehaviour, IManager {

    private ListDataInfo playerDataInfo;
    private ListEnemiesInfo enemiesDataInfo;
    private ListItemsInfo itemDataInfo;
    private EnvironmentData envData;
    private Configuration config;
    private ListWorkInfo workData;
    private ListCreditInfo creditData;

    public void Initialize()
    {
        LoadallData();
        Debug.Log("boot Done " + typeof(GlobalDataManager));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SaveAllData()
    {
        //get data from player attribute and save it to json File
        Utility.WriteDataToJSON<ListDataInfo>(Constants.JSONIndex.DATA_PLAYER, ref playerDataInfo);
        Utility.WriteDataToJSON<ListEnemiesInfo>(Constants.JSONIndex.DATA_ENEMY, ref enemiesDataInfo);
        Utility.WriteDataToJSON<ListItemsInfo>(Constants.JSONIndex.DATA_ITEM, ref itemDataInfo);
        Utility.WriteDataToJSON<EnvironmentData>(Constants.JSONIndex.DATA_ENVIRONMENT, ref envData);
        Utility.WriteDataToJSON<Configuration>(Constants.JSONIndex.DATA_CONFIG, ref config);
    }

    public void LoadallData()
    {
        
        playerDataInfo = Utility.ReadDataFromJSON<ListDataInfo>(Constants.JSONIndex.DATA_PLAYER);
        enemiesDataInfo = Utility.ReadDataFromJSON<ListEnemiesInfo>(Constants.JSONIndex.DATA_ENEMY);
        itemDataInfo = Utility.ReadDataFromJSON<ListItemsInfo>(Constants.JSONIndex.DATA_ITEM);
        envData = Utility.ReadDataFromJSON<EnvironmentData>(Constants.JSONIndex.DATA_ENVIRONMENT);
        workData = Utility.ReadDataFromJSON<ListWorkInfo>(Constants.JSONIndex.DATA_WORK);
        config = Utility.ReadDataFromJSON<Configuration>(Constants.JSONIndex.DATA_CONFIG);
        creditData = Utility.ReadDataFromJSON<ListCreditInfo>(Constants.JSONIndex.DATA_CREDIT);

        Debug.Log("All Data Loaded");
        //Debug.Log(enemiesStatus[0].statsList[0].Agility);
    }

    public void SaveEnvData()
    {
        Utility.WriteDataToJSON<EnvironmentData>(Constants.JSONIndex.DATA_ENVIRONMENT, ref envData);
    }
    public void SaveConfig()
    {
        Utility.WriteDataToJSON<Configuration>(Constants.JSONIndex.DATA_CONFIG, ref config);
    }
    public void NewGame()
    {
        config = new Configuration();
        envData = new EnvironmentData();
        playerDataInfo = new ListDataInfo();
    }
    public void SavePlayerData()
    {
        Utility.WriteDataToJSON<ListDataInfo>(Constants.JSONIndex.DATA_PLAYER, ref playerDataInfo);
    }

    public void SetPlayerDataInfo(Stats stat, SpriteInfo spriteInfo, bool ForceSave = false)
    {
        playerDataInfo.Clear();
        playerDataInfo.statsList.Add(stat);
        playerDataInfo.spriteList.Add(spriteInfo);

        if(ForceSave)
            Utility.WriteDataToJSON<ListDataInfo>(Constants.JSONIndex.DATA_PLAYER, ref playerDataInfo);
    }

    public void SetEnemyDataInfo(ListEnemiesInfo enemyList, bool ForceSave = false)
    {
        enemiesDataInfo = enemyList;

        if (ForceSave)
            Utility.WriteDataToJSON<ListEnemiesInfo>(Constants.JSONIndex.DATA_ENEMY, ref enemiesDataInfo);
    }

    public void SetItemDataInfo(ListItemsInfo itemList, bool ForceSave = false)
    {
        itemDataInfo = itemList;

        if (ForceSave)
            Utility.WriteDataToJSON<ListItemsInfo>(Constants.JSONIndex.DATA_ITEM, ref itemDataInfo);
    }

    public void SetPlayerTier(int tier)
    {
        playerDataInfo.playerTier = tier;
    }

    public ListDataInfo GetPlayerDataInfo()
    {
        return playerDataInfo;
    }
    public ListEnemiesInfo GetEnemyDataInfo()
    {
        return enemiesDataInfo;
    }
    public ListItemsInfo GetItemDataInfo()
    {
        return itemDataInfo;
    }

    public Configuration GetConfiguration()
    {
        return config;
    }
    public EnvironmentData GetEnvData()
    {
        return envData;
    }

    public ListWorkInfo GetAllWorkData()
    {
        return workData;
    }

    public ListCreditInfo GetAllCreditData()
    {
        return creditData;
    }

}
