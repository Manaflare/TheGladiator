﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCreatorManager : CreateCharacterManager
{
    [Header("Enemies")]
    ListEnemiesInfo enemyList;
    public Dropdown enemyDropDown;
    private int tier = 1;
    public Text tierText;
    public int skillPointsPerTier;
    // Use this for initialization
    void Start()
    {
        tierText.text = tier.ToString();
        this.startinAvaliablePoints = (this.BaseStats * skillPointsPerTier) * tier;
        enemyDropDown.ClearOptions();
        enemyList = MasterManager.ManagerGlobalData.GetEnemyDataInfo();

        List<string> data = new List<string>();

        for (int i = 0; i < enemyList.enemyData.Count; i++)
        {
            data.Add(enemyList.enemyData[i].statsList[0].Name);
        }
        enemyDropDown.AddOptions(data);
        Reset();
    }

    public override void StartGame()
    {
        if (this.avaliablePoints < 0 || this.avaliablePoints > 0)
        {
            Debug.LogWarning("You spend more of skills it is avaliable, please rebalance it");
            return;
        }
        Stats playerStats = new Stats(NameText.text, Constants.PlayerType.ENEMY, HPPoints, StrPoints, AgiPoints, DexPoints, StaPoints);
        SpriteInfo playerSpriteInfo = new SpriteInfo(faceHairIndex, hairIndex, bodyIndex);
        ListDataInfo enemy = new ListDataInfo(playerStats, playerSpriteInfo);
        enemy.playerTier = tier;
        enemyList.enemyData[enemyDropDown.value] = enemy;
        MasterManager.ManagerGlobalData.SetEnemyDataInfo(enemyList, true);
    }
    public void ChangeTier(int value)
    {
        if (tier <= 1 && value < 0)
        {
            return;
        }

        tier += value;
        this.startinAvaliablePoints = (this.BaseStats * skillPointsPerTier) * tier;

        int spentPoints = this.HPPoints + this.StrPoints + this.AgiPoints + this.DexPoints + this.StaPoints;
        this.avaliablePoints = startinAvaliablePoints + (BaseStats * 5) - spentPoints;
       
        UpdateStatusText();
    }
    public void LoadEnemy()
    {
        ListDataInfo selectedEnemy = new ListDataInfo();
        for (int i = 0; i < enemyList.enemyData.Count; i++)
        {
            string enemyName = enemyDropDown.options[enemyDropDown.value].text;
            if (enemyList.enemyData[i].statsList[0].Name == enemyName)
            {
                selectedEnemy = enemyList.enemyData[i];
                break;
            }
        }
        Populate(selectedEnemy);
    }

    public void NewPage()
    {
        ListDataInfo newEnemy = new ListDataInfo();
        Stats newEnemyStat = new Stats("New Enemy", Constants.PlayerType.ENEMY, 0, 0, 0, 0, 0);
        SpriteInfo newEnemySprite = new SpriteInfo(0, 0, 0);
        newEnemy.statsList.Add(newEnemyStat);
        newEnemy.spriteList.Add(newEnemySprite);
        enemyList.enemyData.Add(newEnemy);
        newEnemy.playerTier = 1;
        Start();
        enemyDropDown.value = enemyDropDown.options.Count - 1;
    }

    void Populate(ListDataInfo playerData)
    {
        this.NameText.text = playerData.statsList[0].Name;
        this.bodyIndex = playerData.spriteList[0].BodyIndex;
        this.faceHairIndex = playerData.spriteList[0].FaceHairIndex;
        this.hairIndex = playerData.spriteList[0].HairIndex;

        BodyArrowPressed("NONE");
        HairArrowPressed("NONE");
        FaceHairArrowPressed("NONE");

        this.HPPoints = playerData.statsList[0].MAXHP;
        this.StrPoints = playerData.statsList[0].Strength;
        this.AgiPoints = playerData.statsList[0].Agility;
        this.DexPoints = playerData.statsList[0].Dexterity;
        this.StaPoints = playerData.statsList[0].Stamina;

        this.tier = playerData.playerTier;
        this.startinAvaliablePoints = (this.BaseStats * skillPointsPerTier) * tier;
        int spentPoints = this.HPPoints + this.StrPoints + this.AgiPoints + this.DexPoints + this.StaPoints;
        this.avaliablePoints = startinAvaliablePoints + (BaseStats * 5) - spentPoints;
        UpdateStatusText();
    }

    protected override void UpdateStatusText()
    {
        base.UpdateStatusText();
        tierText.text = tier.ToString();
    }
}
