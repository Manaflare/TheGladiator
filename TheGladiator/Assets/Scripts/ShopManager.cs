using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using GameAnalyticsSDK;

public class ShopManager : InventoryManager {

    public Text coins;
    public Text price;
    private EnvironmentData envData;
    List<ItemDataInfo> playerItens;
    int selectedItemID = -1;
    int sellItemId = -1;
    List<GameObject> ItemBlocks;

    public GameObject inventoryHolder;

    public Color buySelectionColor;
    public Color sellSelectionColor;
    public Text actionText;
    bool hasUsedShop;

    // Use this for initialization
    protected override void Start()
    {
        higherColor = new Color(0, .5f, 0);
        lowerColor = new Color(1, 0, 0);
        activeColor = new Color(1, 1, 1);
        desactiveColor = new Color(.8f, .8f, .8f);
        equalsColor = new Color(0, 0, 0);
        hiddenColor = new Color(0, 0, 0, 0);

        Armors = MasterManager.ManagerSprite.ArmorList;
        Helmets = MasterManager.ManagerSprite.HelmetList;
        LeftHands = MasterManager.ManagerSprite.LeftHandList;
        RightHands = MasterManager.ManagerSprite.RightHandList;
        Pants = MasterManager.ManagerSprite.PantsList;
        Shoes = MasterManager.ManagerSprite.ShoesList;

        playerData = MasterManager.ManagerGlobalData.GetPlayerDataInfo();
        playerItens = playerData.itemList;
        List<ItemDataInfo> allItens = MasterManager.ManagerGlobalData.GetItemDataInfo().itemData;
        itemList = new List<ItemDataInfo>();
        ItemBlocks = new List<GameObject>();

        for (int p = 0; p < allItens.Count; p++)
        {
            if(allItens[p].Tier == playerData.playerTier )
            {
                itemList.Add(allItens[p]);
            }
        }

        equipedItems = new List<ItemDataInfo>();
        for (int i = 0; i < playerData.equipedItensId.Count; i++)
        {
            for (int p = 0; p < itemList.Count; p++)
            {
                if (itemList[p].id == playerData.equipedItensId[i])
                {
                    equipedItems.Add(itemList[p]);
                }
            }
        }
        envData = MasterManager.ManagerGlobalData.GetEnvData();
        coins.text = envData.gold.ToString();

        filteredItemList = itemList;
        hasUsedShop = false;
        UpdateItemArea();
    }
    protected override void UpdateText(int id)
    {
        selectedItemID = id;
        sellItemId = -1;
        actionText.text = "Buy";
        base.UpdateText(id);
        price.text = filteredItemList[id].price.ToString();
    }

    void UpdateSellText(int id)
    {
        sellItemId = id;
        selectedItemID = -1;
        price.text = (Math.Round((double)(playerItens[id].price/3))).ToString();
        actionText.text = "Sell";
        itemName.text = playerItens[id].Stats.Name;

        string itemPos = "";
        switch (playerItens[id].Item_type)
        {
            case Constants.ItemIndex.ARMOR:
                itemPos = Constants.C_ARMOR;
                break;
            case Constants.ItemIndex.HELMET:
                itemPos = Constants.C_HELMET;
                break;
            case Constants.ItemIndex.LEFT_HAND:
                itemPos = Constants.C_LEFT_HAND;
                break;
            case Constants.ItemIndex.PANTS:
                itemPos = Constants.C_PANTS;
                break;
            case Constants.ItemIndex.RIGHT_HAND:
                itemPos = Constants.C_RIGHT_HAND;
                break;
            case Constants.ItemIndex.SHOES:
                itemPos = Constants.C_SHOES;
                break;
        }

        ItemDataInfo equipedItem = null;

        for (int i = 0; i < equipedItems.Count; i++)
        {
            if (equipedItems[i].Item_type == playerItens[id].Item_type)
            {
                equipedItem = equipedItems[i];
            }
        }
        if (equipedItem != null && equipedItem.Item_type == playerItens[id].Item_type)
        {
            itemHP.color = playerItens[id].Stats.MAXHP > equipedItem.Stats.MAXHP ? higherColor :
                (playerItens[id].Stats.MAXHP == equipedItem.Stats.MAXHP ? equalsColor : lowerColor);

            itemSTR.color = playerItens[id].Stats.Strength > equipedItem.Stats.Strength ? higherColor :
               (playerItens[id].Stats.Strength == equipedItem.Stats.Strength ? equalsColor : lowerColor);

            itemAGI.color = playerItens[id].Stats.Agility > equipedItem.Stats.Agility ? higherColor :
               (playerItens[id].Stats.Agility == equipedItem.Stats.Agility ? equalsColor : lowerColor);

            itemDEX.color = playerItens[id].Stats.Dexterity > equipedItem.Stats.Dexterity ? higherColor :
               (playerItens[id].Stats.Dexterity == equipedItem.Stats.Dexterity ? equalsColor : lowerColor);

            itemSTA.color = playerItens[id].Stats.Stamina > equipedItem.Stats.Stamina ? higherColor :
               (playerItens[id].Stats.Stamina == equipedItem.Stats.Stamina ? equalsColor : lowerColor);
        }
        else
        {
            itemHP.color = playerItens[id].Stats.MAXHP > 0 ? higherColor : equalsColor;
            itemSTR.color = playerItens[id].Stats.Strength > 0 ? higherColor : equalsColor;
            itemAGI.color = playerItens[id].Stats.Agility > 0 ? higherColor : equalsColor;
            itemDEX.color = playerItens[id].Stats.Dexterity > 0 ? higherColor : equalsColor;
            itemSTA.color = playerItens[id].Stats.Stamina > 0 ? higherColor : equalsColor;
        }

        itemSlot.text = itemPos;

        itemHP.text  = (playerItens[id].Stats.HP).ToString();
        itemSTR.text = playerItens[id].Stats.Strength.ToString();
        itemAGI.text = playerItens[id].Stats.Agility.ToString();
        itemDEX.text = playerItens[id].Stats.Dexterity.ToString();
        itemSTA.text = playerItens[id].Stats.Stamina.ToString();
    }

    protected override void UpdateItemArea()
    {
        ItemBlocks.Clear();

        foreach (Transform child in itemArea.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (Transform child in inventoryHolder.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < filteredItemList.Count; i++)
        {
            GameObject go = Instantiate(ItemBlock);

            int id = i;
            go.AddComponent(typeof(EventTrigger));
            EventTrigger trigger = go.GetComponent<EventTrigger>();

            EventTrigger.Entry clickEntry = new EventTrigger.Entry();
            clickEntry.eventID = EventTriggerType.PointerClick;
            clickEntry.callback.AddListener((eventData) =>
            {
                CleanSelection();
               ((eventData as PointerEventData).pointerPress as GameObject).GetComponentsInChildren<Image>()[0].color = buySelectionColor;
                UpdateText(id);
            });
            trigger.triggers.Add(clickEntry);

            Sprite itemSprite = new Sprite();
            switch (filteredItemList[i].Item_type)
            {
                case Constants.ItemIndex.ARMOR:
                    itemSprite = Armors[filteredItemList[i].Sprite_index];
                    break;
                case Constants.ItemIndex.HELMET:
                    itemSprite = Helmets[filteredItemList[i].Sprite_index];
                    break;
                case Constants.ItemIndex.LEFT_HAND:
                    itemSprite = LeftHands[filteredItemList[i].Sprite_index];
                    break;
                case Constants.ItemIndex.PANTS:
                    itemSprite = Pants[filteredItemList[i].Sprite_index];
                    break;
                case Constants.ItemIndex.RIGHT_HAND:
                    itemSprite = RightHands[filteredItemList[i].Sprite_index];
                    break;
                case Constants.ItemIndex.SHOES:
                    itemSprite = Shoes[filteredItemList[i].Sprite_index];
                    break;
            }
            go.GetComponentsInChildren<Image>()[1].sprite = itemSprite;
            go.transform.SetParent(itemArea.transform, false);
            ItemBlocks.Add(go);
        }

        for (int q = 0; q < playerItens.Count; q++)
        {
            GameObject go = Instantiate(ItemBlock);

            int id = q;
            go.AddComponent(typeof(EventTrigger));
            EventTrigger trigger = go.GetComponent<EventTrigger>();

            EventTrigger.Entry clickEntry = new EventTrigger.Entry();
            clickEntry.eventID = EventTriggerType.PointerClick;
            clickEntry.callback.AddListener((eventData) =>
            {
                CleanSelection();
                ((eventData as PointerEventData).pointerPress as GameObject).GetComponentsInChildren<Image>()[0].color = sellSelectionColor;
                UpdateSellText(id);
            });
            trigger.triggers.Add(clickEntry);

            Sprite itemSprite = new Sprite();
            switch (playerItens[q].Item_type)
            {
                case Constants.ItemIndex.ARMOR:
                    itemSprite = Armors[playerItens[q].Sprite_index];
                    break;
                case Constants.ItemIndex.HELMET:
                    itemSprite = Helmets[playerItens[q].Sprite_index];
                    break;
                case Constants.ItemIndex.LEFT_HAND:
                    itemSprite = LeftHands[playerItens[q].Sprite_index];
                    break;
                case Constants.ItemIndex.PANTS:
                    itemSprite = Pants[playerItens[q].Sprite_index];
                    break;
                case Constants.ItemIndex.RIGHT_HAND:
                    itemSprite = RightHands[playerItens[q].Sprite_index];
                    break;
                case Constants.ItemIndex.SHOES:
                    itemSprite = Shoes[playerItens[q].Sprite_index];
                    break;
            }
            go.GetComponentsInChildren<Image>()[1].sprite = itemSprite;
            go.transform.SetParent(inventoryHolder.transform, false);
            ItemBlocks.Add(go);
        }
    }
    
    public void BuyItem()
    {
        if(selectedItemID > -1 )
        {
            ItemDataInfo item = filteredItemList[selectedItemID];
            if (envData.gold >= item.price)
            {
                //GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "gold", item.price, item.Item_type.ToString() , item.Stats.Name);
                envData.gold -= item.price;
                playerData.itemList.Add(item);
                MasterManager.ManagerGlobalData.SaveAllData();
                TownManager.Instance.UpdatePlayerUI();
                //@Todo PLAY CASH SOUND
                selectedItemID = -1;
                ResetText();
                Start();
                hasUsedShop = true;
            }
            else
            {
                MasterManager.ManagerPopup.ShowMessageBox("Oh No!", "You don't have enougth gold to buy this Item", Constants.PopupType.POPUP_NO);
            }
        }
        else if(sellItemId > -1)
        {
            ItemDataInfo item = playerItens[sellItemId];
            envData.gold += (long)(Math.Round((double)item.price / 3));
            playerData.itemList.Remove(item);
            MasterManager.ManagerGlobalData.SaveAllData();
            TownManager.Instance.UpdatePlayerUI();
            //@Todo PLAY CASH SOUND
            sellItemId = -1;
            ResetText();
            Start();
            hasUsedShop = true;
        }
        else
        {
            MasterManager.ManagerPopup.ShowMessageBox("Oh No!", "You must select an item before clicking here", Constants.PopupType.POPUP_NO);
        }

    }
    private void CleanSelection() {
        for (int i = 0; i < ItemBlocks.Count; i++)
        {
            ItemBlocks[i].GetComponentsInChildren<Image>()[0].color = activeColor;
        }
        selectedItemID = -1;
        sellItemId = -1;
    }

    private void ResetText()
    {
        itemName.text = "";
        itemSlot.text = "--";
        price.text = "0";
        itemHP.text = "0";
        itemSTR.text = "0";
        itemAGI.text = "0";
        itemDEX.text = "0";;
        itemSTA.text = "0";

        itemHP.color =  equalsColor;
        itemSTR.color = equalsColor;
        itemAGI.color = equalsColor;
        itemDEX.color = equalsColor;
        itemSTA.color = equalsColor;
    }

    public override void CloseWindow(bool Spendtime)
    {
        TownManager.Instance.GoBackToMusic();
        TownManager.Instance.CloseCurrentWindow(hasUsedShop);
    }
}
