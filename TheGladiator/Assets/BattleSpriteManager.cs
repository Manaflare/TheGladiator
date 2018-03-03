using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSpriteManager : CharacterSpriteManager {
    // Use this for initialization
    protected override void Start () {
        Attribute temp = this.GetComponent<Attribute>();
        if (temp == null && index == 1)
        {
            index = 0;
            temp = GetComponentInParent<BattleResultScript>().Loser.GetComponent<Attribute>();

        }
        if (temp == null)
        {
            temp = GetComponentInParent<BattleResultScript>().Winner.GetComponent<Attribute>();
            index++;
        }
        if (temp.getSTATS().PlayerType == Constants.PlayerType.ENEMY)
        {
            List<ListDataInfo> e = MasterManager.ManagerGlobalData.GetEnemyDataInfo();
            spriteInfo = e[(int)Constants.ENEMYTierIndex.TIER_1].spriteList[0];

        }
        applySettings();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
