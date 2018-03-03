using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MM = MasterManager;

public class CreateCharacterManager : MonoBehaviour {
    [Header("Sprites Settings")]
    public Image bodyImage;
    public Text bodyText;
    protected int bodyIndex;

    public Image hairImage;
    public Text hairText;
    protected int hairIndex;

    public Image faceHairImage;
    public Text faceHairText;
    protected int faceHairIndex;

    [Header("Atributes Visual")]
    public Text avaliableText;
    public Text HPText;
    public Text StrText;
    public Text AgiText;
    public Text DexText;
    public Text StaText;
    [Header("Atributes")]
    public int startinAvaliablePoints;
    private int avaliablePoints;

    public int HPMultiplyer;
    public int BaseStats;

    protected int HPPoints;
    protected byte StrPoints;
    protected byte AgiPoints;
    protected byte DexPoints;
    protected short StaPoints;

    public Text NameText;


    protected ListDataInfo playerStatusList;

    void Start () {
        playerStatusList = new ListDataInfo();
        Reset();

    }
    public virtual void StartGame()
    {
        if(avaliablePoints > 0 || NameText.text == "")
        {
            //@TODO Show Error message
            return;
        }

        Stats playerStats = new Stats(NameText.text,Constants.PlayerType.PLAYER,HPPoints,StrPoints,AgiPoints,DexPoints,StaPoints);
        SpriteInfo playerSpriteInfo = new SpriteInfo(faceHairIndex, hairIndex, bodyIndex);
        MasterManager.ManagerGlobalData.SetPlayerDataInfo(playerStats, playerSpriteInfo,true);
    }

    public void Reset()
    {
        bodyIndex = 0;
        bodyText.text = bodyIndex.ToString();
        bodyImage.sprite = SpriteManager.Instance.BodyList[bodyIndex];

        hairIndex = 0;
        hairText.text = hairIndex.ToString();
        hairImage.color = new Color(0, 0, 0, 0);

        faceHairIndex = 0;
        faceHairText.text = faceHairIndex.ToString();
        faceHairImage.color = new Color(0, 0, 0, 0);

        avaliablePoints = startinAvaliablePoints;
        HPPoints = BaseStats;
        StrPoints = AgiPoints = DexPoints = (byte)BaseStats;
        StaPoints = (short)BaseStats;

        UpdateStatusText();
    }

    // Update is called once per frame
    void Update () {

    }
    
    public void Randomize()
    {
        Reset();

        bodyIndex = Random.Range(0, SpriteManager.Instance.BodyList.Count - 1);
        hairIndex = Random.Range(0, SpriteManager.Instance.HairList.Count - 1);
        faceHairIndex = Random.Range(0, SpriteManager.Instance.FacialHairList.Count - 1);

        BodyArrowPressed("");
        HairArrowPressed("");
        FaceHairArrowPressed("");

        while(avaliablePoints > 0)
        {
            int index = Random.Range(0, 5);
            AddAttributes(index);
        }
    }

    public void BodyArrowPressed(string direction)
    {
        if(direction == "LEFT")
        {
            bodyIndex--;
            if (bodyIndex < 0)
            {
                bodyIndex = SpriteManager.Instance.BodyList.Count - 1;
            }
        }
        else
        {
            bodyIndex++;
            if (bodyIndex > SpriteManager.Instance.BodyList.Count - 1)
            {
                bodyIndex = 0;
            }
        }
        bodyText.text = bodyIndex.ToString();
        bodyImage.sprite = SpriteManager.Instance.BodyList[bodyIndex];
    }

    public void HairArrowPressed(string direction)
    {
        if (direction == "LEFT")
        {
            hairIndex--;
            if (hairIndex < 0)
            {
                hairIndex = SpriteManager.Instance.HairList.Count - 1;
            }
        }
        else
        {
            hairIndex++;
            if (hairIndex > SpriteManager.Instance.HairList.Count - 1)
            {
                hairIndex = 0;
            }
        }

        if (hairIndex == 0)
        {
            hairImage.color = new Color(0, 0, 0, 0);
        }
        else
        {
            hairImage.color = new Color(1, 1, 1, 1);
            hairImage.sprite = SpriteManager.Instance.HairList[hairIndex];
        }

        hairText.text = hairIndex.ToString();
    }

    public void FaceHairArrowPressed(string direction)
    {
        if (direction == "LEFT")
        {
            faceHairIndex--;
            if (faceHairIndex < 0)
            {
                faceHairIndex = SpriteManager.Instance.FacialHairList.Count;
            }
        }
        else
        {
            faceHairIndex++;
            if (faceHairIndex > SpriteManager.Instance.FacialHairList.Count)
            {
                faceHairIndex = 0;
            }
        }
       
        faceHairText.text = faceHairIndex.ToString();
        if (faceHairIndex == 0)
        {
            faceHairImage.color = new Color(0, 0, 0, 0);
        }
        else
        {
            faceHairImage.color = new Color(1, 1, 1, 1);
            faceHairImage.sprite = SpriteManager.Instance.FacialHairList[faceHairIndex];
        }
    }


    /**
     * 
     * ATRIBUTES
     * 
     **/
    public void AddAttributes(int attribute)
    {

        if(avaliablePoints <= 0)
        {
            return;
        }
        avaliablePoints--;
        if (attribute == (int)Constants.AttributeTypes.HP)
        {
            HPPoints++;
        }
        else if (attribute == (int)Constants.AttributeTypes.STR)
        {
            StrPoints++;
        }
        else if (attribute == (int)Constants.AttributeTypes.AGI)
        {
            AgiPoints++;
        }
        else if (attribute == (int)Constants.AttributeTypes.DEX)
        {
            DexPoints++;
        }
        else if (attribute == (int)Constants.AttributeTypes.STA)
        {
            StaPoints++;
        }
        UpdateStatusText();
    }
    public void RemoveAttributes(int attribute)
    {
        if (attribute == (int)Constants.AttributeTypes.HP)
        {
            if(HPPoints > BaseStats)
            {
                HPPoints--;
                avaliablePoints++;
            }
        }
        else if (attribute == (int)Constants.AttributeTypes.STR)
        {
            if (StrPoints > BaseStats)
            {
                StrPoints--;
                avaliablePoints++;
            }
        }
        else if (attribute == (int)Constants.AttributeTypes.AGI)
        {
            if (AgiPoints > BaseStats)
            {
                AgiPoints--;
                avaliablePoints++;
            }
        }
        else if (attribute == (int)Constants.AttributeTypes.DEX)
        {
            if (DexPoints > BaseStats)
            {
                DexPoints--;
                avaliablePoints++;
            }
        }
        else if (attribute == (int)Constants.AttributeTypes.STA)
        {
            if (StaPoints > BaseStats)
            {
                StaPoints--;
                avaliablePoints++;
            }
        }
        UpdateStatusText();
    }
    void UpdateStatusText()
    {
        avaliableText.text = avaliablePoints.ToString();
        HPText.text = (HPMultiplyer * HPPoints).ToString();
        StrText.text = StrPoints.ToString();
        AgiText.text = AgiPoints.ToString();
        DexText.text = DexPoints.ToString();
        StaText.text = StaPoints.ToString();
    }
}
