using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ListStatus
{
    public List<Stats> statsList;
}
[System.Serializable]
public struct Stats
{
    public float HP;
    public float MAXHP;
    public byte Strength;
    public byte Agility;
    public byte Dexterity;
    public short Stamina;
}

public class Attribute : MonoBehaviour
{

    public Stats STATS { get; private set; }

    public bool IsAlive { get; private set; }
    public bool IsDying { get; private set; }

    // Use this for initialization
    protected virtual void Start ()
    {
		
	}

    // Update is called once per frame
    protected virtual void Update ()
    {
		
	}
    
    public void takeDamage(byte damage)
    {
        HP -= damage;
    }
}
