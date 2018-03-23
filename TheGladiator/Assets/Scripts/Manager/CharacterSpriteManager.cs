using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSpriteManager : MonoBehaviour {

    public GameObject prefabPart;
    Image body;
    Image hair;
    Image facehair;
    Image armor;
    Image helmet;
    Image rightHand;
    Image leftHand;
    Image foot;
    Image pants;

    protected ListDataInfo playerData;
    protected List<ItemDataInfo> itemList;
    protected SpriteInfo spriteInfo;

    Color showColor = new Color(1, 1, 1, 1);
    Color hideColor = new Color(1, 1, 1, 0);

    // Use this for initialization
    protected virtual void Start ()
    {
        foreach (Transform child in this.gameObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        loadImages();
        playerData = MasterManager.ManagerGlobalData.GetPlayerDataInfo();
        itemList = playerData.itemList;
        spriteInfo = playerData.spriteList.Count > 0 ? playerData.spriteList[0]:null;
        applySettings();
    }

    private void OnEnable()
    {
        Start();
    }

    public void loadImages()
    {
        body      = Instantiate(prefabPart, this.transform).GetComponent<Image>();
        armor = Instantiate(prefabPart, this.transform).GetComponent<Image>();
        hair = Instantiate(prefabPart, this.transform).GetComponent<Image>();
        facehair = Instantiate(prefabPart, this.transform).GetComponent<Image>();
        helmet = Instantiate(prefabPart, this.transform).GetComponent<Image>();
        pants     = Instantiate(prefabPart, this.transform).GetComponent<Image>();
        foot = Instantiate(prefabPart, this.transform).GetComponent<Image>();
        leftHand = Instantiate(prefabPart, this.transform).GetComponent<Image>();
        rightHand = Instantiate(prefabPart, this.transform).GetComponent<Image>();

        body.gameObject.name = "Body";
        hair.gameObject.name = "Hair";
        facehair.gameObject.name = "Facial Hair";
        armor.gameObject.name = "Armor";
        helmet.gameObject.name = "Helmet";
        rightHand.gameObject.name = "Right Hand";
        leftHand.gameObject.name = "Left Hand";
        foot.gameObject.name = "Foot";
        pants.gameObject.name = "Pants";
    }

    public void applySettings()
    {
        if(spriteInfo == null)
        {
            return;
        }
        itemList = playerData.itemList;
        spriteInfo = playerData.spriteList[0];
        GetSpriteFromManager(body, spriteInfo.BodyIndex, Constants.SpriteType.BODY);
        GetSpriteFromManager(hair, spriteInfo.HairIndex, Constants.SpriteType.HAIR);
        GetSpriteFromManager(facehair, spriteInfo.FaceHairIndex, Constants.SpriteType.FACIAL_HAIR);

        for (int p = 0; p < playerData.equipedItensId.Count; p++)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (itemList[i].id == playerData.equipedItensId[p])
                {
                    switch (itemList[i].Item_type)
                    {
                        case Constants.ItemIndex.RIGHT_HAND:
                            GetSpriteFromManager(rightHand, itemList[i].Sprite_index, Constants.SpriteType.RIGHT_HAND);
                            break;
                        case Constants.ItemIndex.ARMOR:
                            GetSpriteFromManager(armor, itemList[i].Sprite_index, Constants.SpriteType.ARMOR);
                            break;
                        case Constants.ItemIndex.LEFT_HAND:
                            GetSpriteFromManager(leftHand, itemList[i].Sprite_index, Constants.SpriteType.LEFT_HAND);
                            break;
                        case Constants.ItemIndex.HELMET:
                            GetSpriteFromManager(helmet, itemList[i].Sprite_index, Constants.SpriteType.HELMET);
                            break;
                        case Constants.ItemIndex.PANTS:
                            GetSpriteFromManager(pants, itemList[i].Sprite_index, Constants.SpriteType.PANTS);
                            break;
                        case Constants.ItemIndex.SHOES:
                            GetSpriteFromManager(foot, itemList[i].Sprite_index, Constants.SpriteType.FOOT);
                            break;
                    }
                }
            }
        }
    }
    public void UpdateSprites()
    {
        Start();
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
