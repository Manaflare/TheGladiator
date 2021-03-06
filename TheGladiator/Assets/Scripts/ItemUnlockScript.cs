﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//currently set to use a winstreak to call it, not really properly set up yet
public class ItemUnlockScript : MonoBehaviour
{
    private ListDataInfo playerDataInfo;
    private ListItemsInfo itemDataInfo;
    private ListDataInfo playerItems;
    List<ItemDataInfo> itemData;

    private int currentTier;
    private int itemTier;
    private int rngCannon;

    public Text itemName;
    public Text itemHP;
    public Text itemSTR;
    public Text itemAGI;
    public Text itemDEX;
    public Text itemSTA;
    
    public Image itemSprite;

    public Button continueButton;

    // Use this for initialization
    void Start ()
    {
        rngCannon = Random.Range(0, 100);

        Button btn = continueButton.GetComponent<Button>();
        btn.onClick.AddListener(continueButtonClick);

        playerDataInfo = MasterManager.ManagerGlobalData.GetPlayerDataInfo();
        currentTier = playerDataInfo.playerTier == 0 ? 1 : playerDataInfo.playerTier;

        itemDataInfo = MasterManager.ManagerGlobalData.GetItemDataInfo();
        itemData = new List<ItemDataInfo>();

        fireTheCannon();
    }
    void continueButtonClick()
    {
        this.transform.gameObject.SetActive(false);
    }

    void fireTheCannon()
    {
        if (rngCannon < 50)
        {
            itemTier = currentTier == 1 ? currentTier : currentTier - 1;
            
        }
        else if (rngCannon > 50 && rngCannon < 85)
        {
            itemTier = currentTier;
        }
        else
        {
            itemTier = currentTier + 1;
        }

        for (int i = 0; i < itemDataInfo.itemData.Count; i++)
        {
            if (itemDataInfo.itemData[i].Tier == itemTier)
            {
                itemData.Add(itemDataInfo.itemData[i]);
            }
        }

        int itemID = Random.Range(0, itemData.Count - 1);

        itemName.text = itemData[itemID].Stats.Name.ToString();
        itemHP.text = itemData[itemID].Stats.HP.ToString();
        itemSTR.text = itemData[itemID].Stats.Strength.ToString();
        itemAGI.text = itemData[itemID].Stats.Agility.ToString();
        itemDEX.text = itemData[itemID].Stats.Dexterity.ToString();
        itemSTA.text = itemData[itemID].Stats.Stamina.ToString();

        switch (itemData[itemID].Item_type)
        {
            case Constants.ItemIndex.ARMOR:
                itemSprite.sprite = MasterManager.ManagerSprite.ArmorList[itemData[itemID].Sprite_index];
                break;
            case Constants.ItemIndex.HELMET:
                itemSprite.sprite = MasterManager.ManagerSprite.HelmetList[itemData[itemID].Sprite_index];
                break;
            case Constants.ItemIndex.LEFT_HAND:
                itemSprite.sprite = MasterManager.ManagerSprite.LeftHandList[itemData[itemID].Sprite_index];
                break;
            case Constants.ItemIndex.PANTS:
                itemSprite.sprite = MasterManager.ManagerSprite.PantsList[itemData[itemID].Sprite_index];
                break;
            case Constants.ItemIndex.RIGHT_HAND:
                itemSprite.sprite = MasterManager.ManagerSprite.RightHandList[itemData[itemID].Sprite_index];
                break;
            case Constants.ItemIndex.SHOES:
                itemSprite.sprite = MasterManager.ManagerSprite.ShoesList[itemData[itemID].Sprite_index];
                break;
        }

        playerItems = MasterManager.ManagerGlobalData.GetPlayerDataInfo();
        playerItems.itemList.Add(itemData[itemID]);
        
    }
}
