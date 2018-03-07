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
    protected static int index = 0;
    protected SpriteInfo spriteInfo;

    Color showColor = new Color(1, 1, 1, 1);
    Color hideColor = new Color(1, 1, 1, 0);


    private void Awake()
    {


    }
    // Use this for initialization
    protected virtual void Start ()
    {
        loadImages();
        spriteInfo = MasterManager.ManagerGlobalData.GetPlayerDataInfo().spriteList[0];
        applySettings();
    }
    void loadImages()
    {
        body      = Instantiate(prefabPart, this.transform).GetComponent<Image>();
        hair      = Instantiate(prefabPart, this.transform).GetComponent<Image>();
        facehair  = Instantiate(prefabPart, this.transform).GetComponent<Image>();
        armor     = Instantiate(prefabPart, this.transform).GetComponent<Image>();
        helmet    = Instantiate(prefabPart, this.transform).GetComponent<Image>();
        rightHand = Instantiate(prefabPart, this.transform).GetComponent<Image>();
        leftHand  = Instantiate(prefabPart, this.transform).GetComponent<Image>();
        foot      = Instantiate(prefabPart, this.transform).GetComponent<Image>();
        pants     = Instantiate(prefabPart, this.transform).GetComponent<Image>();

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
