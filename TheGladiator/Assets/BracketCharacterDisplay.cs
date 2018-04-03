using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BracketCharacterDisplay : CharacterSpriteManager {

	// Use this for initialization
	protected override void Start () {
        //base.Start();
            //loadImages();
            //playerData = MasterManager.ManagerGlobalData.GetPlayerDataInfo();
        
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
