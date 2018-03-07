using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePopupCharacterDsiplay : CharacterSpriteManager 
{
    // Use this for initialization
    protected override void Start()
    {

    }

    public void SetSprite(SpriteInfo _spriteInfo)
    {
        loadImages();
        spriteInfo = _spriteInfo;
        applySettings();
    }
}
