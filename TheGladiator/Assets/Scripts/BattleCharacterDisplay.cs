using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacterDisplay : CharacterSpriteManager {
    // Use this for initialization
    public int indexToDraw = 0;
	protected override void Start () {
        //loadImages();
        //Attribute player1 = GetComponent<PlayerAttribute>();
        //if (player1 == null)
        //{
        //    try
        //    {
        //        playerData = MasterManager.ManagerGlobalData.GetEnemyDataInfo().enemyData[indexToDraw];
        //    }
        //    catch { }
        //}
        //else
        //{
        //    try
        //    {

        //    playerData = MasterManager.ManagerGlobalData.GetPlayerDataInfo();
        //    }
        //    catch { }
        //}

        //applySettings();


    }
    public void Draw(ListDataInfo draw)
    {
        loadImages();
        playerData = new ListDataInfo(draw);
        applySettings();
    }
    // Update is called once per frame
    void Update () {
		
	}
}
