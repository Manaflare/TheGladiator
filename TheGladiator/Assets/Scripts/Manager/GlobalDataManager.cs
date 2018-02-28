using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataManager : MonoBehaviour, IManager {

    private ListStatus playerStatus;
    private List<ListStatus> enemiesStatus;

    public void Initialize()
    {
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
        Utility.WriteStatsToJSON(Constants.JSONIndex.DATA_PLAYER, ref playerStatus);
        for (int i = (int)Constants.JSONIndex.DATA_ENEMY_TIER_1; i < (int)Constants.JSONIndex.DATA_ENEMY_TIER_MAX; ++i)
        {
            ListStatus enemyStatus = enemiesStatus[i];
            Utility.WriteStatsToJSON((Constants.JSONIndex)(i), ref enemyStatus);
        }

    }

    public void LoadallData()
    {
        playerStatus = Utility.ReadStatsFromJSON(Constants.JSONIndex.DATA_PLAYER);

        for (int i = (int)Constants.JSONIndex.DATA_ENEMY_TIER_1; i < (int)Constants.JSONIndex.DATA_ENEMY_TIER_MAX; ++i)
        {
            ListStatus enemyStatus = new ListStatus();
            enemyStatus = Utility.ReadStatsFromJSON((Constants.JSONIndex)i);
            enemiesStatus.Add(enemyStatus);
        }
    }

    public void SetPlayerStatus(Stats stat)
    {
        playerStatus.statsList.Add(stat);
        Utility.WriteStatsToJSON(Constants.JSONIndex.DATA_PLAYER, ref playerStatus);
    }

}
