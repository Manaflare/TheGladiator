using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacterDisplay : CharacterSpriteManager {
	// Use this for initialization
	protected override void Start () {
        loadImages();
        Attribute player1 = GetComponent<PlayerAttribute>();

        if (player1 == null)
        {
            spriteInfo = MasterManager.ManagerGlobalData.GetEnemyDataInfo().enemyData[0].spriteList[0];
        }
        else
        {
            spriteInfo = MasterManager.ManagerGlobalData.GetPlayerDataInfo().spriteList[0];
        }

        applySettings();


    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
