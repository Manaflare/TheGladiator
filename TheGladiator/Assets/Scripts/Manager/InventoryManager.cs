using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{

    public GameObject itemArea;
    public GameObject ItemBlock;
    public GameObject Tabs;
    public GameObject Character;

    protected Color higherColor;
    protected Color lowerColor;
    protected Color equalsColor;
    protected Color activeColor;
    protected Color desactiveColor;
    protected Color hiddenColor;

    public Image armorImage;
    public Image helmetImage;
    public Image rHandImage;
    public Image lHandImage;
    public Image footImage;
    public Image pantsImage;

    public Text itemName;
    public Text itemSlot;
    public Text itemHP;
    public Text itemSTR;
    public Text itemAGI;
    public Text itemDEX;
    public Text itemSTA;

    protected ListDataInfo playerData;
    protected List<ItemDataInfo> equipedItems;
    protected List<ItemDataInfo> filteredItemList;
    protected List<ItemDataInfo> itemList;

    protected List<Sprite> Armors;
    protected List<Sprite> Helmets;
    protected List<Sprite> LeftHands;
    protected List<Sprite> RightHands;
    protected List<Sprite> Pants;
    protected List<Sprite> Shoes;

    // declare variable for BGM
    public AudioClip backgroundMusic;
    
    // Use this for initialization
    protected virtual void  Start()
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

        equipedItems = new List<ItemDataInfo>();
        playerData = MasterManager.ManagerGlobalData.GetPlayerDataInfo();
        filteredItemList = itemList = playerData.itemList;

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
        UpdateItemArea();
        UpdateEquipedVisual();

       
    }
    // plays music when the Inventory is opened
    void OnEnable()
    {
        Start();
        // call BGM
        if(backgroundMusic)
            MasterManager.ManagerSound.PlayBackgroundMusic(backgroundMusic);
    }
    protected virtual void UpdateItemArea()
    {
        foreach (Transform child in itemArea.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < filteredItemList.Count; i++)
        {
            GameObject go = Instantiate(ItemBlock);

            int id = i;
            go.AddComponent(typeof(EventTrigger));
            EventTrigger trigger = go.GetComponent<EventTrigger>();

            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((eventData) =>
            {
                UpdateText(id);
            });
            trigger.triggers.Add(entry);

            EventTrigger.Entry clickEntry = new EventTrigger.Entry();
            clickEntry.eventID = EventTriggerType.PointerClick;
            clickEntry.callback.AddListener((eventData) =>
            {
                EquipItem(id);
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
        }
    }
    public void UnequipItem(int itemType)
    {
        for (int i = 0; i < equipedItems.Count; i++)
        {
            if ((int)equipedItems[i].Item_type == itemType)
            {
                equipedItems[i] = null;
                equipedItems.RemoveAt(i);
            }
        }
        SavePlayerData();
        UpdateEquipedVisual();
    }
    void EquipItem(int id)
    {
        ItemDataInfo tempItem = filteredItemList[id];
        bool replaced = false;
        
        for (int i = 0; i < equipedItems.Count; i++)
        {
            if(equipedItems[i].Item_type == tempItem.Item_type)
            {
                equipedItems[i] = null;
                equipedItems[i] = tempItem;
                replaced = true;
            }
        }

        if (!replaced)
        {
            equipedItems.Add(tempItem);
        }
        SavePlayerData();
        UpdateEquipedVisual();
    }
    void SavePlayerData()
    {
        List<int> equipedItemsId = new List<int>();

        for (int i = 0; i < equipedItems.Count; i++)
        {
            equipedItemsId.Add(equipedItems[i].id);
        }

        playerData.equipedItensId = equipedItemsId;
        MasterManager.ManagerGlobalData.SavePlayerData();
    }
    void UpdateEquipedVisual()
    {

        try
        {
            rHandImage.sprite = null;
            rHandImage.color = hiddenColor;
            armorImage.sprite = null;
            armorImage.color = hiddenColor;
            lHandImage.sprite = null;
            lHandImage.color = hiddenColor;
            helmetImage.sprite = null;
            helmetImage.color = hiddenColor;
            pantsImage.sprite = null;
            pantsImage.color = hiddenColor;
            footImage.sprite = null;
            footImage.color = hiddenColor;
        }
        catch 
        {
            return;
        }

        

        for (int i = 0; i < equipedItems.Count; i++)
        {
            switch (equipedItems[i].Item_type)
            {
                case Constants.ItemIndex.RIGHT_HAND:
                    rHandImage.sprite = RightHands[equipedItems[i].Sprite_index];
                    rHandImage.color = activeColor;
                    break;
                case Constants.ItemIndex.ARMOR:
                    armorImage.sprite = Armors[equipedItems[i].Sprite_index];
                    armorImage.color = activeColor;
                    break;
                case Constants.ItemIndex.LEFT_HAND:
                    lHandImage.sprite = LeftHands[equipedItems[i].Sprite_index];
                    lHandImage.color = activeColor;
                    break;
                case Constants.ItemIndex.HELMET:
                    helmetImage.sprite = Helmets[equipedItems[i].Sprite_index];
                    helmetImage.color = activeColor;
                    break;
                case Constants.ItemIndex.PANTS:
                    pantsImage.sprite = Pants[equipedItems[i].Sprite_index];
                    pantsImage.color = activeColor;
                    break;
                case Constants.ItemIndex.SHOES:
                    footImage.sprite = Shoes[equipedItems[i].Sprite_index];
                    footImage.color = activeColor;
                    break;
            }
        }
        Character.GetComponent<CharacterSpriteManager>().UpdateSprites();
    }
    public void UpdateTab(int tabIndex)
    {
        foreach (Transform child in Tabs.transform)
        {
            child.GetComponent<Image>().color = desactiveColor;
        }

        Tabs.transform.GetChild(tabIndex).GetComponent<Image>().color = activeColor;

    }
    public void FilterItems(int filter)
    {
        filteredItemList = null;
        filteredItemList = new List<ItemDataInfo>();

        if (filter == -1)
        {
            filteredItemList = itemList;
        }
        else
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if ((int)itemList[i].Item_type == filter)
                {
                    filteredItemList.Add(itemList[i]);
                }
            }
        }
        UpdateItemArea();
    }
    protected virtual void UpdateText(int id)
    {
        itemName.text = filteredItemList[id].Stats.Name;

        string itemPos = "";
        switch (filteredItemList[id].Item_type)
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
            if (equipedItems[i].Item_type == filteredItemList[id].Item_type)
            {
                equipedItem = equipedItems[i];
            }
        }
        if (equipedItem != null && equipedItem.Item_type == filteredItemList[id].Item_type)
        {
            itemHP.color = filteredItemList[id].Stats.MAXHP > equipedItem.Stats.MAXHP ? higherColor :
                (filteredItemList[id].Stats.MAXHP == equipedItem.Stats.MAXHP ? equalsColor : lowerColor);

            itemSTR.color = filteredItemList[id].Stats.Strength > equipedItem.Stats.Strength ? higherColor :
               (filteredItemList[id].Stats.Strength == equipedItem.Stats.Strength ? equalsColor : lowerColor);

            itemAGI.color = filteredItemList[id].Stats.Agility > equipedItem.Stats.Agility ? higherColor :
               (filteredItemList[id].Stats.Agility == equipedItem.Stats.Agility ? equalsColor : lowerColor);

            itemDEX.color = filteredItemList[id].Stats.Dexterity > equipedItem.Stats.Dexterity ? higherColor :
               (filteredItemList[id].Stats.Dexterity == equipedItem.Stats.Dexterity ? equalsColor : lowerColor);

            itemSTA.color = filteredItemList[id].Stats.Stamina > equipedItem.Stats.Stamina ? higherColor :
               (filteredItemList[id].Stats.Stamina == equipedItem.Stats.Stamina ? equalsColor : lowerColor);
        }
        else
        {
            itemHP.color =  filteredItemList[id].Stats.MAXHP >     0?higherColor:equalsColor;
            itemSTR.color = filteredItemList[id].Stats.Strength >  0?higherColor:equalsColor;
            itemAGI.color = filteredItemList[id].Stats.Agility >   0?higherColor:equalsColor;
            itemDEX.color = filteredItemList[id].Stats.Dexterity > 0?higherColor:equalsColor;
            itemSTA.color = filteredItemList[id].Stats.Stamina >   0?higherColor:equalsColor;
        }

        itemSlot.text = itemPos;

        itemHP.text = (Constants.HP_MULTIPLIER * filteredItemList[id].Stats.HP).ToString();
        itemSTR.text = filteredItemList[id].Stats.Strength.ToString();
        itemAGI.text = filteredItemList[id].Stats.Agility.ToString();
        itemDEX.text = filteredItemList[id].Stats.Dexterity.ToString();
        itemSTA.text = filteredItemList[id].Stats.Stamina.ToString();
    }
    public void CloseWindow(bool Spendtime)
    {
        SavePlayerData();
        TownManager.Instance.GoBackToMusic();
        TownManager.Instance.CloseCurrentWindow(Spendtime);
    }
}
