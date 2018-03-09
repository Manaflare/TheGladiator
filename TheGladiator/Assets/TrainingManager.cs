using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TrainingManager : MonoBehaviour
{
    public Text text_MaxHp;
    public Text text_agil;
    public Text text_stam;
    public Text text_str;
    public Text text_dex;

    public Text NameText;

    protected int HPPoints;
    protected byte StrPoints;
    protected byte AgiPoints;
    protected byte DexPoints;
    protected short StaPoints;

    public Image faceHairImage;
    public Text faceHairText;
    protected int faceHairIndex;

    public Image hairImage;
    public Text hairText;
    protected int hairIndex;
    public Image bodyImage;
    public Text bodyText;
    protected int bodyIndex;

    [Header("Training Effectivness Settings")]
    public int minChange;
    public int maxChange;
    public GameObject TrainingCompletionPrefab;

    // Use this for initialization
    void Start()
    {
         ListDataInfo playerDataInfo = MasterManager.ManagerGlobalData.GetPlayerDataInfo();
         int agil = playerDataInfo.statsList[0].Agility;
         text_agil.text = agil.ToString();

         int stam = playerDataInfo.statsList[0].Stamina;
         text_stam.text = stam.ToString();

         int str = playerDataInfo.statsList[0].Strength;
         text_str.text = str.ToString();

         int dex = playerDataInfo.statsList[0].Dexterity;
         text_dex.text = dex.ToString();

         int MaxHp = playerDataInfo.statsList[0].MAXHP;
         text_MaxHp.text = MaxHp.ToString();
          


        hairIndex = playerDataInfo.spriteList[0].HairIndex;
        hairImage.sprite = SpriteManager.Instance.HairList[hairIndex];
       


        //Stats playerStats = new Stats(NameText.text, Constants.PlayerType.PLAYER, HPPoints, StrPoints, AgiPoints, DexPoints, StaPoints);
        //SpriteInfo playerSpriteInfo = new SpriteInfo(faceHairIndex, hairIndex, bodyIndex);
        //MasterManager.ManagerGlobalData.SetPlayerDataInfo(playerStats, playerSpriteInfo, true);

    }


    public void TrainStrength()
    {
        byte ammountAdd = (byte) Random.Range(minChange, maxChange);
        MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0].Strength += ammountAdd;
        ShowTrainingCompletion();
    }

    public void TrainMaxHP()
    {
        byte ammountAdd = (byte)Random.Range(minChange, maxChange);
        MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0].MAXHP += ammountAdd;
        ShowTrainingCompletion();
    }

    public void TrainAgility()
    {
        byte ammountAdd = (byte)Random.Range(minChange, maxChange);
        MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0].Agility += ammountAdd;
        ShowTrainingCompletion();
    }

    public void TrainDexterity()
    {
        byte ammountAdd = (byte)Random.Range(minChange, maxChange);
        MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0].Dexterity += ammountAdd;
        ShowTrainingCompletion();
    }

    public void TrainStamina()
    {
        byte ammountAdd = (byte)Random.Range(minChange, maxChange);
        MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0].MaxStamina += ammountAdd;
        ShowTrainingCompletion();
    }

    void ShowTrainingCompletion()
    {
        TrainingCompletionPrefab.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
