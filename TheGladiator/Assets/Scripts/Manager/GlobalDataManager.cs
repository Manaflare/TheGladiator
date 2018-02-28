using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataManager : MonoBehaviour, IManager {

    private ListDataInfo playerStatus;
    private List<ListDataInfo> enemiesStatus;

    public void Initialize()
    {
        enemiesStatus = new List<ListDataInfo>();
        LoadallData();
        Debug.Log("boot Done " + typeof(GlobalDataManager));

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SaveAllData()
    {
        //get data from player attribute and save it to json File
        Utility.WriteDataInfoToJSON(Constants.JSONIndex.DATA_PLAYER, ref playerStatus);
        for (int i = (int)Constants.JSONIndex.DATA_ENEMY_TIER_1; i < (int)Constants.JSONIndex.DATA_ENEMY_TIER_MAX; ++i)
        {
            ListDataInfo enemyStatus = enemiesStatus[i];
            Utility.WriteDataInfoToJSON((Constants.JSONIndex)(i), ref enemyStatus);
        }

    }

    public void LoadallData()
    {
        playerStatus = Utility.ReadDataInfoFromJSON(Constants.JSONIndex.DATA_PLAYER);

        for (int i = (int)Constants.JSONIndex.DATA_ENEMY_TIER_1; i < (int)Constants.JSONIndex.DATA_ENEMY_TIER_MAX; ++i)
        {
          
            ListDataInfo enemyStatus = new ListDataInfo();
            enemyStatus = Utility.ReadDataInfoFromJSON((Constants.JSONIndex)i);
            enemiesStatus.Add(enemyStatus);
        }

        Debug.Log(playerStatus.spriteList[0].FaceHairIndex);
        Debug.Log(enemiesStatus[0].statsList[0].Agility);
    }

    public void SetPlayerDataInfo(Stats stat, SpriteInfo spriteInfo)
    {
        playerStatus.Clear();
        playerStatus.statsList.Add(stat);
        playerStatus.spriteList.Add(spriteInfo);

        Debug.Log(playerStatus.spriteList[0].FaceHairIndex);
        Utility.WriteDataInfoToJSON(Constants.JSONIndex.DATA_PLAYER, ref playerStatus);
    }

}
