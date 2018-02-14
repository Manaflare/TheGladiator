using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ListStatus
{
    public List<Stats> statsList;
}
[System.Serializable]
public class Stats
{
    public Constants.Name name;
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
