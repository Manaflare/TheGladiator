using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    protected static int index = 0;
    protected SpriteInfo spriteInfo;

    Color showColor = new Color(1, 1, 1, 1);
    Color hideColor = new Color(1, 1, 1, 0);


    // Use this for initialization
    protected virtual void Start ()
    {
        Attribute temp = this.GetComponent<Attribute>();
        if (temp == null && index == 0)
        {
            temp = GetComponentInParent<BattleResultScript>().Winner.GetComponent<Attribute>();
            index++;
        }
        if (temp.getSTATS().PlayerType == Constants.PlayerType.ENEMY)
        {
            List<ListDataInfo> e = MasterManager.ManagerGlobalData.GetEnemyDataInfo();
            spriteInfo = e[(int)Constants.ENEMYTierIndex.TIER_1].spriteList[0];

        }
        if (temp == null && index == 1)
        {
            index = 0;
            temp = GetComponentInParent<BattleResultScript>().Loser.GetComponent<Attribute>();

        }
        else
        {
            spriteInfo = MasterManager.ManagerGlobalData.GetPlayerDataInfo().spriteList[0];
        }
        applySettings();
    }
    void applySettings()
    {
        GetSpriteFromManager(body, spriteInfo.BodyIndex, Constants.SpriteType.BODY);
        GetSpriteFromManager(hair, spriteInfo.HairIndex, Constants.SpriteType.HAIR);
        GetSpriteFromManager(facehair, spriteInfo.FaceHairIndex, Constants.SpriteType.FACIAL_HAIR);

        armor.color = hideColor;
        helmet.color = hideColor;
        rightHand.color = hideColor;
        leftHand.color = hideColor;
        rightHand.color = hideColor;
        foot.color = hideColor;
        pants.color = hideColor;
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
