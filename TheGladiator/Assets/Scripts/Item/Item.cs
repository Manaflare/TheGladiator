using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour {

    public GameObject Owner;
    public GameObject Effect;
    public List<int> values;

    public enum INDEX_VALUE
    {
        HP,
        STR,
        AGI,
        DEX
    }

    public int index;
    public int spriteIndex;
    public string title;
    public string descripition;

    // Use this for initialization
    protected virtual void Start ()
    {
		
	}

    // Update is called once per frame
    protected virtual void Update ()
    {
		
	}

}
