using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    protected int avaliablePoints;

    public int HPMultiplyer;
    public int BaseStats;

    protected int HPPoints;
    protected byte StrPoints;
    protected byte AgiPoints;
    protected byte DexPoints;
    protected short StaPoints;

    // declare variable for BGM
    public AudioClip backgroundMusic;

    [Header("Name")]
    public InputField NameText;
    protected ListDataInfo playerStatusList;

    void Start ()
    {
        // call BGM
        MasterManager.ManagerSound.PlayBackgroundMusic(backgroundMusic);

        playerStatusList = new ListDataInfo();
        Reset();

    }
    public virtual void StartGame()
    {
        if(avaliablePoints > 0 || NameText.text == "")
        {
            string errorMessage = "";
            if(NameText.text == "")
            {
                errorMessage = "Please name your character";
            }
            else
            {
                errorMessage = "Please use all your avaliable points";
            }

            MM.ManagerPopup.ShowMessageBox("Hey, Listen!", errorMessage, Constants.PopupType.POPUP_NO);
            return;
        }

        // call SFX
        MasterManager.ManagerSound.PlaySingleSound("Menu Confirm");

        MasterManager.ManagerGlobalData.NewGame();

        Stats playerStats = new Stats(NameText.text,Constants.PlayerType.PLAYER,HPPoints,StrPoints,AgiPoints,DexPoints,StaPoints);
        SpriteInfo playerSpriteInfo = new SpriteInfo(faceHairIndex, hairIndex, bodyIndex);
        MasterManager.ManagerGlobalData.SetPlayerDataInfo(playerStats, playerSpriteInfo,true);
        MasterManager.ManagerLoadScene.LoadScene("Town");
    }

    public void Reset(bool clearName = true)
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

        if (clearName)
        {
            NameText.text = "";
        }

        UpdateStatusText();
    }

    // Update is called once per frame
    void Update () {

    }
    
    public void Back()
    {
        MasterManager.ManagerLoadScene.LoadScene("MainMenu", false);
    }

    public void Randomize()
    {
        Reset(false);

        bodyIndex = Random.Range(0, SpriteManager.Instance.BodyList.Count - 1);
        hairIndex = Random.Range(0, SpriteManager.Instance.HairList.Count - 1);
        faceHairIndex = Random.Range(0, SpriteManager.Instance.FacialHairList.Count - 1);

        BodyArrowPressed("NONE");
        HairArrowPressed("NONE");
        FaceHairArrowPressed("NONE");

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
            // call SFX
            MasterManager.ManagerSound.PlaySingleSound("Menu Select");
        }
        else if (direction == "NONE")
        {

        }
        else
        {
            bodyIndex++;
            if (bodyIndex > SpriteManager.Instance.BodyList.Count - 1)
            {
                bodyIndex = 0;
            }
            // call SFX
            MasterManager.ManagerSound.PlaySingleSound("Menu Select");
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
            // call SFX
            MasterManager.ManagerSound.PlaySingleSound("Menu Select");
        }
        else if (direction == "NONE")
        {

        }else
        {
            hairIndex++;
            if (hairIndex > SpriteManager.Instance.HairList.Count - 1)
            {
                hairIndex = 0;
            }
            // call SFX
            MasterManager.ManagerSound.PlaySingleSound("Menu Select");
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
            // call SFX
            MasterManager.ManagerSound.PlaySingleSound("Menu Select");
        }
        else if (direction == "NONE")
        {

        }
        else
        {
            faceHairIndex++;
            if (faceHairIndex > SpriteManager.Instance.FacialHairList.Count)
            {
                faceHairIndex = 0;
            }
            // call SFX
            MasterManager.ManagerSound.PlaySingleSound("Menu Select");
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
            // call SFX
            MasterManager.ManagerSound.PlaySingleSound("Menu Select");
        }
        else if (attribute == (int)Constants.AttributeTypes.STR)
        {
            StrPoints++;
            // call SFX
            MasterManager.ManagerSound.PlaySingleSound("Menu Select");
        }
        else if (attribute == (int)Constants.AttributeTypes.AGI)
        {
            AgiPoints++;
            // call SFX
            MasterManager.ManagerSound.PlaySingleSound("Menu Select");
        }
        else if (attribute == (int)Constants.AttributeTypes.DEX)
        {
            DexPoints++;
            // call SFX
            MasterManager.ManagerSound.PlaySingleSound("Menu Select");
        }
        else if (attribute == (int)Constants.AttributeTypes.STA)
        {
            StaPoints++;
            // call SFX
            MasterManager.ManagerSound.PlaySingleSound("Menu Select");
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
                // call SFX
                MasterManager.ManagerSound.PlaySingleSound("Menu Select");
            }
        }
        else if (attribute == (int)Constants.AttributeTypes.STR)
        {
            if (StrPoints > BaseStats)
            {
                StrPoints--;
                avaliablePoints++;
                // call SFX
                MasterManager.ManagerSound.PlaySingleSound("Menu Select");
            }
        }
        else if (attribute == (int)Constants.AttributeTypes.AGI)
        {
            if (AgiPoints > BaseStats)
            {
                AgiPoints--;
                avaliablePoints++;
                // call SFX
                MasterManager.ManagerSound.PlaySingleSound("Menu Select");
            }
        }
        else if (attribute == (int)Constants.AttributeTypes.DEX)
        {
            if (DexPoints > BaseStats)
            {
                DexPoints--;
                avaliablePoints++;
                // call SFX
                MasterManager.ManagerSound.PlaySingleSound("Menu Select");
            }
        }
        else if (attribute == (int)Constants.AttributeTypes.STA)
        {
            if (StaPoints > BaseStats)
            {
                StaPoints--;
                avaliablePoints++;
                // call SFX
                MasterManager.ManagerSound.PlaySingleSound("Menu Select");
            }
        }
        UpdateStatusText();
    }
    protected virtual void UpdateStatusText()
    {
        avaliableText.text = avaliablePoints.ToString();
        HPText.text = (HPMultiplyer * HPPoints).ToString();
        StrText.text = StrPoints.ToString();
        AgiText.text = AgiPoints.ToString();
        DexText.text = DexPoints.ToString();
        StaText.text = StaPoints.ToString();
    }
}
