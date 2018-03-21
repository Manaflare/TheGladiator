using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

    public GameObject itemArea;
    public GameObject ItemBlock;

    public Text itemName;
    public Text itemSlot;
    public Text itemHP;
    public Text itemSTR;
    public Text itemAGI;
    public Text itemDEX;
    public Text itemSTA;


    ListDataInfo playerData;
    List<ItemDataInfo> itemList;
        
    private List<Sprite> Armors;
    private List<Sprite> Helmets;
    private List<Sprite> LeftHands;
    private List<Sprite> RightHands;
    private List<Sprite> Pants;
    private List<Sprite> Shoes;
    // Use this for initialization
    void Start () {

        Armors = MasterManager.ManagerSprite.ArmorList;
        Helmets = MasterManager.ManagerSprite.HelmetList;
        LeftHands = MasterManager.ManagerSprite.LeftHandList;
        RightHands = MasterManager.ManagerSprite.RightHandList;
        Pants = MasterManager.ManagerSprite.PantsList;
        Shoes = MasterManager.ManagerSprite.ShoesList;

        playerData = MasterManager.ManagerGlobalData.GetPlayerDataInfo();
        itemList = MasterManager.ManagerGlobalData.GetItemDataInfo().itemData;

        for (int i = 0; i < itemList.Count; i++)
        {
            GameObject go = Instantiate(ItemBlock);

            int id = i;
            go.AddComponent(typeof(EventTrigger));
            EventTrigger trigger = go.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((eventData) => {
                UpdateText(id);
            });
            trigger.triggers.Add(entry);
            
            Sprite itemSprite = new Sprite();
            switch (itemList[i].Item_type)
            {
                case Constants.ItemIndex.ARMOR:
                    itemSprite = Armors[itemList[i].Sprite_index];
                    break;
                case Constants.ItemIndex.HELMET:
                    itemSprite = Helmets[itemList[i].Sprite_index];
                    break;
                case Constants.ItemIndex.LEFT_HAND:
                    itemSprite = LeftHands[itemList[i].Sprite_index];
                    break;
                case Constants.ItemIndex.PANTS:
                    itemSprite = Pants[itemList[i].Sprite_index];
                    break;
                case Constants.ItemIndex.RIGHT_HAND:
                    itemSprite = RightHands[itemList[i].Sprite_index];
                    break;
                case Constants.ItemIndex.SHOES:
                    itemSprite = Shoes[itemList[i].Sprite_index];
                    break;
            }
            go.GetComponentsInChildren<Image>()[1].sprite = itemSprite;
            go.transform.SetParent(itemArea.transform, false);
        }
        UpdateText(0);
    }

    void UpdateText(int id)
    {
        itemName.text = itemList[id].Stats.Name;

        //itemSlot.text = itemList[id].Stats.Name;
        itemHP.text = itemList[id].Stats.HP.ToString();
        itemSTR.text = itemList[id].Stats.Strength.ToString();
        itemAGI.text = itemList[id].Stats.Agility.ToString();
        itemDEX.text = itemList[id].Stats.Dexterity.ToString();
        itemSTA.text = itemList[id].Stats.Stamina.ToString();
    }
}
