using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataManager : MonoBehaviour, IManager {

    private ListDataInfo playerDataInfo;
    private List<ListDataInfo> enemiesDataInfo;

    public void Initialize()
    {
        enemiesDataInfo = new List<ListDataInfo>();
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
        Utility.WriteDataInfoToJSON(Constants.JSONIndex.DATA_PLAYER, ref playerDataInfo);
        for (int i = (int)Constants.JSONIndex.DATA_ENEMY_TIER_1; i < (int)Constants.JSONIndex.DATA_ENEMY_TIER_MAX; ++i)
        {
            ListDataInfo enemyStatus = enemiesDataInfo[i];
            Utility.WriteDataInfoToJSON((Constants.JSONIndex)(i), ref enemyStatus);
        }

    }

    public void LoadallData()
    {
        playerDataInfo = Utility.ReadDataInfoFromJSON(Constants.JSONIndex.DATA_PLAYER);

        for (int i = (int)Constants.JSONIndex.DATA_ENEMY_TIER_1; i < (int)Constants.JSONIndex.DATA_ENEMY_TIER_MAX; ++i)
        {

            ListDataInfo enemyStatus = new ListDataInfo();
            enemyStatus = Utility.ReadDataInfoFromJSON((Constants.JSONIndex)i);
            enemiesDataInfo.Add(enemyStatus);
        }

        //Debug.Log(playerStatus.spriteList[0].FaceHairIndex);
        //Debug.Log(enemiesStatus[0].statsList[0].Agility);
    }

    public void SetPlayerDataInfo(Stats stat, SpriteInfo spriteInfo, bool ForceSave = false)
    {
        playerDataInfo.Clear();
        playerDataInfo.statsList.Add(stat);
        playerDataInfo.spriteList.Add(spriteInfo);

        if(ForceSave)
            Utility.WriteDataInfoToJSON(Constants.JSONIndex.DATA_PLAYER, ref playerDataInfo);
    }

    public void SetPlayerTier(int tier)
    {
        playerDataInfo.playerTier = tier;
    }

    public ListDataInfo GetPlayerDataInfo()
    {
        return playerDataInfo;
    }

    public List<ListDataInfo> GetEnemyDataInfo()
    {
        return enemiesDataInfo;
    }

}
