using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ItemCreationManager : MonoBehaviour {


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

    [Header("Inputs")]
    public InputField NameText;
    public InputField PriceText;

    [Header("Dropdown")]
    public Dropdown ItemDropDown;
    public Dropdown TypeDropDown;

    [Header("Tier")]
    public int tier = 0;
    public Text tierText;
    public int skillPointsPerTier;

    [Header("Popup")]
    public GameObject PopUp;
    public GameObject BlockHolder;
    public GameObject ItemBlock;
    public Image SelectedSprite;

    private List<Sprite> Armors;
    private List<Sprite> Helmets;
    private List<Sprite> LeftHands;
    private List<Sprite> RightHands;
    private List<Sprite> Pants;
    private List<Sprite> Shoes;

    private ListItemsInfo itemList;
    private int spriteIndex;

    // Use this for initialization
    void Start () {
        Armors = MasterManager.ManagerSprite.ArmorList;
        Helmets = MasterManager.ManagerSprite.HelmetList;
        LeftHands = MasterManager.ManagerSprite.LeftHandList;
        RightHands = MasterManager.ManagerSprite.RightHandList;
        Pants = MasterManager.ManagerSprite.PantsList;
        Shoes = MasterManager.ManagerSprite.ShoesList;
        ItemDropDown.ClearOptions();

        itemList = MasterManager.ManagerGlobalData.GetItemDataInfo();

        List<string> items = new List<string>();
        for (int i = 0; i < itemList.itemData.Count; i++)
        {
            items.Add(itemList.itemData[i].Stats.Name);
        }
        ItemDropDown.AddOptions(items);

        List<string> types = new List<string>();
        types.Add(Constants.RIGHT_HAND);
        types.Add(Constants.ARMOR);
        types.Add(Constants.LEFT_HAND);
        types.Add(Constants.HELMET);
        types.Add(Constants.PANTS);
        types.Add(Constants.SHOES);
        tierText.text = tier.ToString();

        TypeDropDown.ClearOptions();
        TypeDropDown.AddOptions(types);

        Reset();
    }
    public void LoadItem()
    {
        Populate(itemList.itemData[ItemDropDown.value]);
    }
    void Populate(ItemDataInfo itemData)
    {
        this.NameText.text = itemData.Stats.Name;
        this.PriceText.text = itemData.price.ToString();

        this.HPPoints = itemData.Stats.HP;
        this.StrPoints = itemData.Stats.Strength;
        this.AgiPoints = itemData.Stats.Agility;
        this.DexPoints = itemData.Stats.Dexterity;
        this.StaPoints = itemData.Stats.Stamina;
        spriteIndex = itemData.Sprite_index;
        TypeDropDown.value = (int)itemData.Item_type;

        switch (itemData.Item_type)
        {
            case Constants.ItemIndex.ARMOR:
                SelectedSprite.sprite = Armors[itemData.Sprite_index];
                break;
            case Constants.ItemIndex.HELMET:
                SelectedSprite.sprite = Helmets[itemData.Sprite_index];
                break;
            case Constants.ItemIndex.LEFT_HAND:
                SelectedSprite.sprite = LeftHands[itemData.Sprite_index];
                break;
            case Constants.ItemIndex.PANTS:
                SelectedSprite.sprite = Pants[itemData.Sprite_index];
                break;
            case Constants.ItemIndex.RIGHT_HAND:
                SelectedSprite.sprite = RightHands[itemData.Sprite_index];
                break;
            case Constants.ItemIndex.SHOES:
                SelectedSprite.sprite = Shoes[itemData.Sprite_index];
                break;
        }

        this.tier = itemData.Tier;
        this.startinAvaliablePoints = (skillPointsPerTier) * tier;
        int spentPoints = this.HPPoints + this.StrPoints + this.AgiPoints + this.DexPoints + this.StaPoints;
        this.avaliablePoints = startinAvaliablePoints + (BaseStats * 5) - spentPoints;
        UpdateStatusText();
    }
    public void New()
    {
        ItemDataInfo newItem = new ItemDataInfo();
        Stats newItemStats = new Stats("New Item",  0, 0, 0, 0, 0, 0);
        newItem.Stats = newItemStats;
        itemList.itemData.Add(newItem);
        newItem.Tier = 1;
        Start();
        ItemDropDown.value = ItemDropDown.options.Count - 1;
        LoadItem();
    }
    public void ShowPopUp()
    {
        PopUp.SetActive(true);
        List<Sprite> spriteList = new List<Sprite>();
        switch (TypeDropDown.value)
        {
            case 1:
                spriteList = Armors;
                break;
            case 3:
                spriteList = Helmets;
                break;
            case 0:
                spriteList = RightHands;
                break;
            case 2:
                spriteList = LeftHands;
                break;
            case 4:
                spriteList = Pants;
                break;
            case 5:
                spriteList = Shoes;
                break;
        }

        for (int i = 0; i < spriteList.Count; i++)
        {
            GameObject go = Instantiate(ItemBlock);

            go.AddComponent(typeof(EventTrigger));
            EventTrigger trigger = go.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((eventData) => {
                SelectedSprite.sprite = (eventData as PointerEventData).pointerPress.GetComponentsInChildren<Image>()[1].sprite;
                spriteIndex = int.Parse(SelectedSprite.sprite.name.Split('_')[1]);
                HidePopUp();
            });
            trigger.triggers.Add(entry);

            go.GetComponentsInChildren<Image>()[1].sprite = spriteList[i];
            go.transform.SetParent(BlockHolder.transform,false);
        }
    }
    public void HidePopUp()
    {
        foreach (Transform child in BlockHolder.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        PopUp.SetActive(false);

    }
    public void ChangeType()
    {
        SelectedSprite.sprite = null;
    }
    public void Randomize()
    {
        Reset(false);

        while (avaliablePoints > 0)
        {
            int index = Random.Range(0, 5);
            AddAttributes(index);
        }
    }
    public void Reset(bool clearName = true)
    {
        avaliablePoints = startinAvaliablePoints;
        HPPoints = BaseStats;
        StrPoints = AgiPoints = DexPoints = (byte)BaseStats;
        StaPoints = (short)BaseStats;

        if (clearName)
        {
            NameText.text = "";
        }
        PriceText.text = "";

        UpdateStatusText();
    }
    public void Save()
    {
        ItemDataInfo itemData = new ItemDataInfo();
        itemData.Item_type = (Constants.ItemIndex)TypeDropDown.value;
        itemData.Stats = new Stats(NameText.text, HPPoints, StrPoints, AgiPoints, DexPoints, StaPoints);
        itemData.Tier = tier;
        itemData.Sprite_index = spriteIndex;
        itemData.id = ItemDropDown.value;
        itemData.price = int.Parse(PriceText.text);
        itemList.itemData[ItemDropDown.value] = itemData;
        MasterManager.ManagerGlobalData.SetItemDataInfo(itemList, true);
        Start();
        LoadItem();
    }
    /**
    * 
    * ATRIBUTES
    * 
    **/
    public void AddAttributes(int attribute)
    {

        if (avaliablePoints <= 0)
        {
            return;
        }
        avaliablePoints--;
        if (attribute == (int)Constants.AttributeTypes.HP)
        {
            HPPoints++;
        }
        else if (attribute == (int)Constants.AttributeTypes.STR)
        {
            StrPoints++;
        }
        else if (attribute == (int)Constants.AttributeTypes.AGI)
        {
            AgiPoints++;
        }
        else if (attribute == (int)Constants.AttributeTypes.DEX)
        {
            DexPoints++;
        }
        else if (attribute == (int)Constants.AttributeTypes.STA)
        {
            StaPoints++;
        }
        UpdateStatusText();
    }
    public void RemoveAttributes(int attribute)
    {
        if (attribute == (int)Constants.AttributeTypes.HP)
        {
            if (HPPoints > BaseStats)
            {
                HPPoints--;
                avaliablePoints++;
            }
        }
        else if (attribute == (int)Constants.AttributeTypes.STR)
        {
            if (StrPoints > BaseStats)
            {
                StrPoints--;
                avaliablePoints++;
            }
        }
        else if (attribute == (int)Constants.AttributeTypes.AGI)
        {
            if (AgiPoints > BaseStats)
            {
                AgiPoints--;
                avaliablePoints++;
            }
        }
        else if (attribute == (int)Constants.AttributeTypes.DEX)
        {
            if (DexPoints > BaseStats)
            {
                DexPoints--;
                avaliablePoints++;
            }
        }
        else if (attribute == (int)Constants.AttributeTypes.STA)
        {
            if (StaPoints > BaseStats)
            {
                StaPoints--;
                avaliablePoints++;
            }
        }
        UpdateStatusText();
    }
    public void ChangeTier(int value)
    {
        if (tier <= 1 && value < 0)
        {
            return;
        }

        tier += value;
        this.startinAvaliablePoints = (skillPointsPerTier) * tier;

        int spentPoints = this.HPPoints + this.StrPoints + this.AgiPoints + this.DexPoints + this.StaPoints;
        this.avaliablePoints = startinAvaliablePoints + (BaseStats * 5) - spentPoints;

        UpdateStatusText();
    }
    private void UpdateStatusText()
    {
        tierText.text = tier.ToString();
        avaliableText.text = avaliablePoints.ToString();
        HPText.text = (HPMultiplyer * HPPoints).ToString();
        StrText.text = StrPoints.ToString();
        AgiText.text = AgiPoints.ToString();
        DexText.text = DexPoints.ToString();
        StaText.text = StaPoints.ToString();
    }
}
