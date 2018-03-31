using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDisplayScript : CharacterSpriteManager
{

    // Use this for initialization
    protected override void Start()
    {
        loadImages();
        Attribute player1 = GetComponent<PlayerAttribute>();

        if (player1 == null)
        {
            playerData = MasterManager.ManagerGlobalData.GetEnemyDataInfo().enemyData[0];
        }
        else
        {
            playerData = MasterManager.ManagerGlobalData.GetPlayerDataInfo();
        }

        applySettings();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
