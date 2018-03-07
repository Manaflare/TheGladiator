using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataManager : MonoBehaviour, IManager {

    private ListDataInfo playerDataInfo;
    private ListEnemiesInfo enemiesDataInfo;

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
    }

    public void LoadallData()
    {
        playerDataInfo = Utility.ReadDataFromJSON<ListDataInfo>(Constants.JSONIndex.DATA_PLAYER);
        enemiesDataInfo = Utility.ReadDataFromJSON<ListEnemiesInfo>(Constants.JSONIndex.DATA_ENEMY);

        //Debug.Log(playerStatus.spriteList[0].FaceHairIndex);
        //Debug.Log(enemiesStatus[0].statsList[0].Agility);
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

}
