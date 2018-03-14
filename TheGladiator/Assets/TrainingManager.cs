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

    [Header("Training Completion Stat Change")]
    public Text OldAgility;
    public Text NewAgility;
    public Text OldDexterity;
    public Text NewDexterity;
    public Text OldMaxHP;
    public Text NewMaxHP;
    public Text OldStr;
    public Text NewStr;
    public Text OldStam;
    public Text NewStam;

    [Header("Training Effectivness Settings")]
    public int minChange;
    public int maxChange;
    public GameObject TrainingCompletionPrefab;

    // Use this for initialization
    void OnEnable()
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

        TrainingCompletionPrefab.SetActive(false);
        //Stats playerStats = new Stats(NameText.text, Constants.PlayerType.PLAYER, HPPoints, StrPoints, AgiPoints, DexPoints, StaPoints);
        //SpriteInfo playerSpriteInfo = new SpriteInfo(faceHairIndex, hairIndex, bodyIndex);
        //MasterManager.ManagerGlobalData.SetPlayerDataInfo(playerStats, playerSpriteInfo, true);

    }

    private void TrainStat(Text originText, Text newText, int multiplier = 1)
    {
        //for variables in train complete window
        OldMaxHP.text = NewMaxHP.text =  text_MaxHp.text;
        OldStr.text = NewStr.text =  text_str.text;
        OldAgility.text = NewAgility.text =  text_agil.text;
        OldDexterity.text = NewDexterity.text =  text_dex.text;
        OldStam.text = NewStam.text = text_stam.text;

        int oldStat = int.Parse(originText.text);
        int newStat = oldStat + ((Random.Range(1, 4) * multiplier));

        newText.text = newStat.ToString();
    }


    public void TrainStrength()
    {
        byte ammountAdd = (byte) Random.Range(minChange, maxChange);
        MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0].Strength += ammountAdd;
        ShowTrainingCompletion();

        TrainStat(text_str, NewStr);
    
    }

    public void TrainMaxHP()
    {
        byte ammountAdd = (byte)Random.Range(minChange, maxChange);
        MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0].MAXHP += ammountAdd;
        ShowTrainingCompletion();
        
        TrainStat(text_MaxHp, NewMaxHP, 5);
       

    }

    public void TrainAgility()
    {
        byte ammountAdd = (byte)Random.Range(minChange, maxChange);
        MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0].Agility += ammountAdd;
        ShowTrainingCompletion();

        TrainStat(text_agil, NewAgility);
       
    }

    public void TrainDexterity()
    {
        byte ammountAdd = (byte)Random.Range(minChange, maxChange);
        MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0].Dexterity += ammountAdd;
        ShowTrainingCompletion();

        TrainStat(text_dex, NewDexterity);
       
        
    }

    public void TrainStamina()
    {
        byte ammountAdd = (byte)Random.Range(minChange, maxChange);
        MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0].MaxStamina += ammountAdd;
        ShowTrainingCompletion();

        TrainStat(text_stam, NewStam);
     
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
