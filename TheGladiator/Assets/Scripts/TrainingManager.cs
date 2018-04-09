using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class TrainingManager : MonoBehaviour
{
    public Text text_MaxHp;
    public Text text_agil;
    public Text text_MaxStam;
    public Text text_str;
    public Text text_dex;
    public Text text_stam;
    public Text text_gold;

    public Text ShowCost;

    public Text NameText;
    public int HpMultiplier = 5;
    protected int HPPoints;
    protected byte StrPoints;
    protected byte AgiPoints;
    protected byte DexPoints;
    protected short MaxStaPoints;

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

    public Text NewAgility;
    public Text NewDexterity;
    public Text NewMaxHP;
    public Text NewStr;
    public Text NewMaxStam;
    public Text CostStaminaText;
    public Text CostGoldText;
    public int CostStaminaBase;
    public int CostGoldBase;
    public int CostBase;
    int CostStamina;
    int CostGold;

    [Header("Training Effectivness Settings")]
    public int minChange;
    public int maxChange;
    public GameObject TrainingCompletionPrefab;

    [Header("Button Visibility")]
    public Button MHp_btn;
    public Button Str_btn;
    public Button Agi_btn;
    public Button Dex_btn;
    public Button Sta_btn;

    // Use this for initialization
    void OnEnable()
    {
       
         ListDataInfo playerDataInfo = MasterManager.ManagerGlobalData.GetPlayerDataInfo();
         int agil = playerDataInfo.statsList[0].Agility;
         text_agil.text = NewAgility.text = agil.ToString();
        
         if(agil == byte.MaxValue)
        {
            Agi_btn.gameObject.SetActive(false);
        }
         else
        {
            Agi_btn.enabled = true;
        }

         int Maxstam = playerDataInfo.statsList[0].MaxStamina;
         text_MaxStam.text = NewMaxStam.text = Maxstam.ToString();
         if(Maxstam == byte.MaxValue)
        {
            Sta_btn.gameObject.SetActive(false);
        }
        else
        {
            Sta_btn.enabled = true;
        }

         int str = playerDataInfo.statsList[0].Strength;
         text_str.text = NewStr.text = str.ToString();
        if(str == byte.MaxValue)
        {
            Str_btn.gameObject.SetActive(false);
        }
        else
        {
            Str_btn.enabled = true;
        }


         int dex = playerDataInfo.statsList[0].Dexterity;
         text_dex.text = NewDexterity.text = dex.ToString();
         if(dex == byte.MaxValue)
        {
            Dex_btn.gameObject.SetActive(false);
        }
         else
        {
            Dex_btn.enabled = true;
        }


         int MaxHp = playerDataInfo.statsList[0].MAXHP * HpMultiplier;
         text_MaxHp.text = NewMaxHP.text = MaxHp.ToString();
         if(MaxHp == byte.MaxValue)
        {
            MHp_btn.gameObject.SetActive(false);
        }
         else
        {
            MHp_btn.enabled = true;
        }

        int totalPoints = playerDataInfo.statsList[0].MAXHP + playerDataInfo.statsList[0].Strength + playerDataInfo.statsList[0].Agility + playerDataInfo.statsList[0].Dexterity + playerDataInfo.statsList[0].MaxStamina;
        CostStamina = Mathf.RoundToInt(playerDataInfo.statsList[0].MaxStamina * 0.3f);
        CostGold = Mathf.RoundToInt((totalPoints / CostBase) * CostGoldBase);
        CostStaminaText.text = CostStamina.ToString();
        CostGoldText.text = CostGold.ToString();


        TrainingCompletionPrefab.SetActive(false);
        //Stats playerStats = new Stats(NameText.text, Constants.PlayerType.PLAYER, HPPoints, StrPoints, AgiPoints, DexPoints, StaPoints);
        //SpriteInfo playerSpriteInfo = new SpriteInfo(faceHairIndex, hairIndex, bodyIndex);
        //MasterManager.ManagerGlobalData.SetPlayerDataInfo(playerStats, playerSpriteInfo, true);

    }

    void TrainingCost()
    {
        ListDataInfo playerDataInfo = MasterManager.ManagerGlobalData.GetPlayerDataInfo();
        playerDataInfo.statsList[0].Stamina -= (short)(CostStamina);
        //NewStam.text = playerDataInfo.statsList[0].Stamina.ToString();

        EnvironmentData envData = MasterManager.ManagerGlobalData.GetEnvData();
        envData.gold -= CostGold;
        //NewGold.text = envData.gold.ToString("N0");
    }

    bool canTrain()
    {
        bool Result = true;
        ListDataInfo playerDataInfo = MasterManager.ManagerGlobalData.GetPlayerDataInfo();
        if (playerDataInfo.GetActualStats().Stamina < CostStamina)
        {
            MasterManager.ManagerPopup.ShowMessageBox("System", "You don't have enough Stamina!", Constants.PopupType.POPUP_SYSTEM);
            Result = false;
        }
        else
        {
            EnvironmentData envData = MasterManager.ManagerGlobalData.GetEnvData();
            if (envData.gold < CostGold)
            {
                MasterManager.ManagerPopup.ShowMessageBox("System", "You don't have enough Gold!", Constants.PopupType.POPUP_SYSTEM);
                Result = false;
                
            }
        }

        return Result;
    }

    private void TrainStat(Text originText, Text newText, int multiplier = 1)
    {
        //for variables in train complete window

        int oldStat = int.Parse(originText.text);
        int newStat = oldStat + ((Random.Range(1, 4) * multiplier));

        

        newText.text = newStat.ToString();
        ListDataInfo playerDataInfo = MasterManager.ManagerGlobalData.GetPlayerDataInfo();
        playerDataInfo.statsList[0].Agility = byte.Parse(NewAgility.text);
        playerDataInfo.statsList[0].Dexterity = byte.Parse(NewDexterity.text);
        playerDataInfo.statsList[0].MaxStamina = short.Parse(NewMaxStam.text);
        playerDataInfo.statsList[0].Strength = byte.Parse(NewStr.text);
        playerDataInfo.statsList[0].MAXHP = int.Parse(NewMaxHP.text) / HpMultiplier;
        







        MasterManager.ManagerGlobalData.SavePlayerData();
    }

    private void RedText(Text redText)
    {
        ResetAllColor();
        redText.color = new Color(0.0f, 0.62f, 0.0f);

    }
    private void TextColor(Text newText)
    {
        ResetAllColor();
        newText.color = new Color(0.0f, 0.39f, 0.0f);
        

    }

    private void ResetAllColor()
    {
        NewMaxStam.color = new Color(0.0f, 0.0f, 0.0f);
        NewStr.color = new Color(0.0f, 0.0f, 0.0f);
        NewAgility.color = new Color(0.0f, 0.0f, 0.0f);
        NewDexterity.color = new Color(0.0f, 0.0f, 0.0f);
        NewMaxHP.color = new Color(0.0f, 0.0f, 0.0f);
        //NewStam.color = new Color(0.0f, 0.0f, 0.0f);
        //NewGold.color = new Color(0.0f, 0.0f, 0.0f);
        

    }


    public void TrainStrength()
    {
        if (canTrain() == false)
        {
            return;
        }

        byte ammountAdd = (byte) Random.Range(minChange, maxChange);
        int expectedStr = MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0].Strength + ammountAdd;

        if (expectedStr >= byte.MaxValue)
        {
            MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0].Strength = byte.MaxValue;
        }
        else
        {
            MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0].Strength += ammountAdd;
        }
       
        CallBackShowTraining();

        TrainStat(text_str, NewStr);
        TextColor(NewStr);
    
    }

    public void TrainMaxHP()
    {
        if (canTrain() == false)
        {
            return;
        }

        byte ammountAdd = (byte)Random.Range(minChange, maxChange);
        int expectedMaxHp = MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0].MAXHP + ammountAdd;

        if (expectedMaxHp >= byte.MaxValue)
        {
            MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0].MAXHP = byte.MaxValue;
        }
        else
        {
            MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0].MAXHP += ammountAdd;
        }
        
        CallBackShowTraining();
        
        TrainStat(text_MaxHp, NewMaxHP, HpMultiplier);
        TextColor(NewMaxHP);
       

    }

    public void Stamina()
    {

    }

    public void GoldAmmount()
    {
       // RedText(NewGold);
    }

    public void TrainAgility()
    {
        if (canTrain() == false)
        {
            return;
        }

        byte ammountAdd = (byte)Random.Range(minChange, maxChange);
        int expectedAgi = MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0].Agility + ammountAdd;

        if (expectedAgi >= byte.MaxValue)
        {
            MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0].Agility = byte.MaxValue;
        }
        else
        {
            MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0].Agility += ammountAdd;
        }

        CallBackShowTraining();

        TrainStat(text_agil, NewAgility);
        TextColor(NewAgility);
       
    }

    public void TrainDexterity()
    {
        if (canTrain() == false)
        {
            return;
        }

        byte ammountAdd = (byte)Random.Range(minChange, maxChange);
        int expectedDex = MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0].Dexterity + ammountAdd;

        if (expectedDex >= byte.MaxValue)
        {
            MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0].Dexterity = byte.MaxValue;
        }
        else
        {
            MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0].Dexterity += ammountAdd;
        }

        CallBackShowTraining();

        TrainStat(text_dex, NewDexterity);
        TextColor(NewDexterity);
       
        
    }

    public void TrainMaxStamina()
    {
        if (canTrain() == false)
        {
            return;
        }

        byte ammountAdd = (byte)Random.Range(minChange, maxChange);
        int expectedStam = MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0].MaxStamina + ammountAdd;

        if (expectedStam >= short.MaxValue)
        {
            MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0].MaxStamina = short.MaxValue;
        }
        else
        {
            MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0].MaxStamina += ammountAdd;
        }
        
        ShowTrainingCompletion();

        TrainStat(text_MaxStam, NewMaxStam);
        TextColor(NewMaxStam);
     
    }
    

    void ShowTrainingCompletion()
    {
        this.gameObject.SetActive(false);
        TrainingCompletionPrefab.SetActive(true);
        ResetAllColor();
        TrainingCost();
    }

    public void CloseWindow(bool Spendtime)
    {
        TrainingCompletionPrefab.SetActive(false);
        this.gameObject.SetActive(true);
        TownManager.Instance.CloseCurrentWindow(Spendtime);
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
