using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ListDataInfo
{
    public ListDataInfo()
    {
        statsList = new List<Stats>();
        spriteList = new List<SpriteInfo>();
    }

    public void Clear()
    {
        statsList.Clear();
        spriteList.Clear();
    }

    public List<Stats> statsList;
    public List<SpriteInfo> spriteList;
    public List<ItemInfo> itemList;
    public int playerTier;
}

[System.Serializable]
public class SpriteInfo
{
    public int FaceHairIndex;
    public int HairIndex;
    public int BodyIndex;

    public SpriteInfo(int faceHair, int hair, int body)
    {
        FaceHairIndex = faceHair;
        HairIndex = hair;
        BodyIndex = body;
    }
}

[System.Serializable]
public class ItemInfo
{
    public int index;
    public bool isEquiped;
}

[System.Serializable]
public class Stats
{
    public Stats(string name, Constants.PlayerType playerType, int maxHp, byte str, byte agi, byte dex, short stamina)
    {
        Name = name;
        PlayerType = playerType;
        MAXHP = HP = maxHp;
        Strength = str;
        Agility = agi;
        Dexterity = dex;
        Stamina = stamina;
    }
    public string Name;
    public Constants.PlayerType PlayerType;
    public int HP;
    public int MAXHP;
    public byte Strength;
    public byte Agility;
    public byte Dexterity;
    public short Stamina;
}

public class Attribute : MonoBehaviour
{
    public GameObject gorePrefab;

    [SerializeField]
    private Stats STATS;

    public Stats getSTATS()
    {
        return STATS;
    }

    public void setSTATS(Stats newStats)
    {
        STATS = newStats;
    }

    public bool IsAlive { get; private set; }

    public bool IsDying { get; private set; }

    // Use this for initialization
    protected virtual void Start ()
    {
		
	}
    public virtual void onDeath()
    {
        GameObject gore = Instantiate(gorePrefab, GameObject.FindObjectOfType<Canvas>().transform);
        gore.GetComponent<RectTransform>().anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;
        gore.SetActive(true);
    }
    // Update is called once per frame
    protected virtual void Update ()
    {
		
	}
    
    public void takeDamage(byte damage)
    {
        //STATS.HP -= damage;
    }
}
