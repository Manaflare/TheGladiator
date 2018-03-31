using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePopupCharacterDsiplay : CharacterSpriteManager 
{
    // Use this for initialization
    protected override void Start()
    {
        Stats s = GetComponentInParent<BattleResultScript>().winner;
        if (s.PlayerType != Constants.PlayerType.PLAYER)
        {
            loadImages();
            playerData = MasterManager.ManagerGlobalData.GetEnemyDataInfo().enemyData[0];
            applySettings();
        }
        else
        {
            base.Start();
        }        
    }
}
