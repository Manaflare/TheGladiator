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
            try
            {
                playerData = MasterManager.ManagerGlobalData.GetEnemyDataInfo().enemyData[0];
            }
            catch { }
        }
        else
        {
            try
            {

            playerData = MasterManager.ManagerGlobalData.GetPlayerDataInfo();
            }
            catch { }
        }

        applySettings();


    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
