using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePopupCharacterDsiplay : CharacterSpriteManager 
{
    // Use this for initialization
    protected override void Start()
    {
        prefabPart.GetComponent<RectTransform>().sizeDelta = new Vector2(250, 250);
        Stats s = GetComponentInParent<BattleResultScript>().winner;
        if (s.PlayerType != Constants.PlayerType.PLAYER)
        {
            loadImages();
            spriteInfo = MasterManager.ManagerGlobalData.GetEnemyDataInfo().enemyData[0].spriteList[0];
            applySettings();
        }
        else
        {
            base.Start();

        }
        prefabPart.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
        
    }
}
