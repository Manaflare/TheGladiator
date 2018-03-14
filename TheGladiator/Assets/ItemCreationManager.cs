using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    [Header("Name")]
    public InputField NameText;

    public Dropdown ItemDropDown;
    public Dropdown TypeDropDown;

    private List<Sprite> Armors;
    private List<Sprite> Helmets;
    private List<Sprite> LeftHand;
    private List<Sprite> RightHand;
    private List<Sprite> Pants;
    private List<Sprite> Shoes;

    public int tier = 0;
    public Text tierText;
    public int skillPointsPerTier;

    // Use this for initialization
    void Start () {
        Armors = MasterManager.ManagerSprite.ArmorList;
        Helmets = MasterManager.ManagerSprite.HelmetList;
        LeftHand = MasterManager.ManagerSprite.LeftHandList;
        RightHand = MasterManager.ManagerSprite.RightHandList;
        Pants = MasterManager.ManagerSprite.PantsList;
        Shoes = MasterManager.ManagerSprite.ShoesList;

        ItemDropDown.ClearOptions();
        // ItemDropDown.AddOptions();


        List<string> types = new List<string>();
        types.Add(Constants.ARMOR);
        types.Add(Constants.HELMET);
        types.Add(Constants.RIGHT_HAND);
        types.Add(Constants.LEFT_HAND);
        types.Add(Constants.PANTS);
        types.Add(Constants.SHOES);

        tierText.text = tier.ToString();

        TypeDropDown.ClearOptions();
        TypeDropDown.AddOptions(types);


        Reset();
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

        UpdateStatusText();
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
