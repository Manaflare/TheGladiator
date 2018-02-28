using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MM = MasterManager;

public class CharacterSpriteManager : MonoBehaviour {

    public Image body;
    public Image hair;
    public Image facehair;
    public Image armor;
    public Image helmet;
    public Image rightHand;
    public Image leftHand;
    public Image foot;
    public Image pants;

    SpriteInfo spriteInfo;

    Color showColor = new Color(1, 1, 1, 1);
    Color hideColor = new Color(1, 1, 1, 0);

    // Use this for initialization
    void Start () {

        spriteInfo = MasterManager.ManagerGlobalData.GetPlayerDataInfo().spriteList[0];

        GetSpriteFromManager(body, spriteInfo.BodyIndex, Constants.SpriteType.BODY);
        GetSpriteFromManager(hair, spriteInfo.HairIndex,Constants.SpriteType.HAIR);
        GetSpriteFromManager(facehair, spriteInfo.FaceHairIndex, Constants.SpriteType.FACIAL_HAIR);
    }

    void UpdateSprites()
    {

    }
	
    void GetSpriteFromManager(Image img, int index, Constants.SpriteType type)
    {
        Sprite tempSprite = MasterManager.ManagerSprite.GetSprite(index, type);

        if (!tempSprite)
        {
            img.sprite = tempSprite;
        }
        else
        {
            img.color = showColor;
        }
        img.sprite = tempSprite;

    }
    // Update is called once per frame
    void Update () {
		
	}
}
