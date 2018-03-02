using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreatorManager : CreateCharacterManager {

    List<ListDataInfo> enemyList;

    // Use this for initialization
    void Start () {
        enemyList = MasterManager.ManagerGlobalData.GetEnemyDataInfo();
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
