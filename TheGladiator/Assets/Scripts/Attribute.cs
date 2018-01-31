using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ListStatus
{
    public List<Status> statusList;
}
[System.Serializable]
public struct Status
{
    public byte Strength;
    public byte Agility;
    public byte Dexterity;
    public short Stamina;
}

public class Attribute : MonoBehaviour
{

    public Status STATUS { get; private set; }
    public float HP { get; private set; }
    public float MAXHP { get; private set; }
    public float MP { get; private set; }
    public float MAXMP { get; private set; }
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
}
