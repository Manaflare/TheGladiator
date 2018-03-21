﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ListItemsInfo
{
    public ListItemsInfo()
    {
        itemData = new List<ItemDataInfo>();
    }
    public List<ItemDataInfo> itemData;
}

[System.Serializable]
public class ItemDataInfo
{
    public Stats Stats;
    public Constants.ItemIndex Item_type;
    public int Sprite_index;
    public int Tier;
    public int id;

    public int price;
}

[System.Serializable]
public class ListEnemiesInfo
{
    public List<ListDataInfo> enemyData;
}

[System.Serializable]
public class ListDataInfo
{
    public ListDataInfo()
    {
        statsList = new List<Stats>();
        spriteList = new List<SpriteInfo>();
    }
    public ListDataInfo(Stats stats, SpriteInfo sprites)
    {
        statsList = new List<Stats>();
        spriteList = new List<SpriteInfo>();

        statsList.Add(stats);
        spriteList.Add(sprites);
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
    public Stats(string name, Constants.PlayerType playerType, int maxHp, byte str, byte agi, byte dex, short maxStamina, short stamina = short.MinValue, int hp = int.MinValue)
    {
        Name = name;
        PlayerType = playerType;
        MAXHP = HP = maxHp;
        if (hp != int.MinValue)
        {
            HP = hp;
        }
        Strength = str;
        Agility = agi;
        Dexterity = dex;
        MaxStamina = Stamina = maxStamina;
        if (stamina != short.MinValue)
        {
            Stamina = stamina;
        }

    }
    public Stats(string name, int maxHp, byte str, byte agi, byte dex, short maxStamina, short stamina = short.MinValue, int hp = int.MinValue)
    {
        Name = name;
        MAXHP = HP = maxHp;
        if (hp != int.MinValue)
        {
            HP = hp;
        }
        Strength = str;
        Agility = agi;
        Dexterity = dex;

        MaxStamina = Stamina = maxStamina;
        if (stamina != short.MinValue)
        {
            Stamina = stamina;
        }

    }
    public static Stats copy(Stats source)
    {
        return new Stats(source.Name, source.PlayerType, source.MAXHP, source.Strength, source.Agility, source.Dexterity,source.MaxStamina, source.Stamina, source.HP);

    }

    public string Name;
    public Constants.PlayerType PlayerType;
    public int HP;
    public int MAXHP;
    public byte Strength;
    public byte Agility;
    public byte Dexterity;
    public short Stamina;
    public short MaxStamina;
}

public class Attribute : MonoBehaviour
{
    public GameObject gorePrefab;

    [SerializeField]
    private Stats STATS;

    [SerializeField]

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
        this.gameObject.SetActive(false);
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
