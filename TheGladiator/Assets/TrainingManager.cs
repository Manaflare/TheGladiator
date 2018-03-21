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

    public Text ShowCost;

    public Text NameText;
    public int HpMultiplier = 5;
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

    [Header("Button Visibility")]
    public Button MHp_btn;
    public Button Str_btn;
    public Button Agi_btn;
    public Button Dex_btn;
    public Button Stam_btn;

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


         int MaxHp = playerDataInfo.statsList[0].MAXHP * HpMultiplier;
         text_MaxHp.text = MaxHp.ToString();     

         TrainingCompletionPrefab.SetActive(false);
        //Stats playerStats = new Stats(NameText.text, Constants.PlayerType.PLAYER, HPPoints, StrPoints, AgiPoints, DexPoints, StaPoints);
        //SpriteInfo playerSpriteInfo = new SpriteInfo(faceHairIndex, hairIndex, bodyIndex);
        //MasterManager.ManagerGlobalData.SetPlayerDataInfo(playerStats, playerSpriteInfo, true);

    }

    void TrainingCost()
    {

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
        ListDataInfo playerDataInfo = MasterManager.ManagerGlobalData.GetPlayerDataInfo();
        playerDataInfo.statsList[0].Agility = byte.Parse(NewAgility.text);
        playerDataInfo.statsList[0].Dexterity = byte.Parse(NewDexterity.text);
        playerDataInfo.statsList[0].Stamina = short.Parse(NewStam.text);
        playerDataInfo.statsList[0].Strength = byte.Parse(NewStr.text);
        playerDataInfo.statsList[0].MAXHP = int.Parse(NewMaxHP.text) / HpMultiplier;

        MHp_btn.enabled = true;
        Agi_btn.enabled = true;
        Dex_btn.enabled = true;
        Str_btn.enabled = true;
        Stam_btn.enabled = true;




        MasterManager.ManagerGlobalData.SavePlayerData();
    }

    private void TextColor(Text newText)
    {
        ResetAllColor();
        newText.color = new Color(0.0f, 0.39f, 0.0f);
    }

    private void ResetAllColor()
    {
        NewStam.color = new Color(0.0f, 0.0f, 0.0f);
        NewStr.color = new Color(0.0f, 0.0f, 0.0f);
        NewAgility.color = new Color(0.0f, 0.0f, 0.0f);
        NewDexterity.color = new Color(0.0f, 0.0f, 0.0f);
        NewMaxHP.color = new Color(0.0f, 0.0f, 0.0f);

    }


    public void TrainStrength()
    {
        byte ammountAdd = (byte) Random.Range(minChange, maxChange);
        if (MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0].Strength >= byte.MaxValue)
        {
            Str_btn.enabled = false;
        }
        MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0].Strength += ammountAdd;
        ShowTrainingCompletion();

        TrainStat(text_str, NewStr);
        TextColor(NewStr);
    
    }

    public void TrainMaxHP()
    {
        byte ammountAdd = (byte)Random.Range(minChange, maxChange);
        if(MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0].MAXHP >= int.MaxValue)
        {
            MHp_btn.enabled = false;
        }
        MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0].MAXHP += ammountAdd;
        ShowTrainingCompletion();
        
        TrainStat(text_MaxHp, NewMaxHP, HpMultiplier);
        TextColor(NewMaxHP);
       

    }

    public void TrainAgility()
    {
        byte ammountAdd = (byte)Random.Range(minChange, maxChange);
        if(MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0].Agility >= byte.MaxValue)
        {
            Agi_btn.enabled = false;
        }
        MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0].Agility += ammountAdd;
        ShowTrainingCompletion();

        TrainStat(text_agil, NewAgility);
        TextColor(NewAgility);
       
    }

    public void TrainDexterity()
    {
        byte ammountAdd = (byte)Random.Range(minChange, maxChange);
        if (MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0].Dexterity >= byte.MaxValue)
        {
            Dex_btn.enabled = false;
        }
        MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0].Dexterity += ammountAdd;
        ShowTrainingCompletion();

        TrainStat(text_dex, NewDexterity);
        TextColor(NewDexterity);
       
        
    }

    public void TrainStamina()
    {
        byte ammountAdd = (byte)Random.Range(minChange, maxChange);
        if (MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0].MaxStamina >= byte.MaxValue)
        {
            Stam_btn.enabled = false;
        }
        MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0].MaxStamina += ammountAdd;
        ShowTrainingCompletion();

        TrainStat(text_stam, NewStam);
        TextColor(NewStam);
     
    }
    

    void ShowTrainingCompletion()
    {
        TrainingCompletionPrefab.SetActive(true);
        ResetAllColor();
    }

    public void CloseWindow(bool Spendtime)
    {
        TownManager.Instance.CloseCurrentWindow(Spendtime, CallBackShowTraining);
    }

    public void CallBackShowTraining()
    {
        ShowTrainingCompletion();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
