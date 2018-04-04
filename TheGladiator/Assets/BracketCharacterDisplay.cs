using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BracketCharacterDisplay : CharacterSpriteManager {
    protected override void Start()
    {
        
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
