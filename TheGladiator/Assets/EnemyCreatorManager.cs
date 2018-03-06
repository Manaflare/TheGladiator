using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCreatorManager : CreateCharacterManager {

    List<ListDataInfo> enemyList;
    public Dropdown enemyDropDown;
    // Use this for initialization
    void Start () {
        enemyDropDown.ClearOptions();
        enemyList = MasterManager.ManagerGlobalData.GetEnemyDataInfo();

        List<string> data = new List<string>();


        for (int i = 0; i < enemyList.Count; i++)
        {
            data.Add(enemyList[i].statsList[0].Name + "_" + enemyList[i].playerTier);
        }
        enemyDropDown.AddOptions(data);
        Debug.Log(enemyList.ToString());
        Reset();
    }

    public override void StartGame()
    {
        /*
        Stats playerStats = new Stats(NameText.text, Constants.PlayerType.ENEMY, HPPoints, StrPoints, AgiPoints, DexPoints, StaPoints);
        SpriteInfo playerSpriteInfo = new SpriteInfo(faceHairIndex, hairIndex, bodyIndex);
        MasterManager.ManagerGlobalData.SetEnemyDataInfo();*/
    }
    // Update is called once per frame
    void Update () {
		
	}
}
