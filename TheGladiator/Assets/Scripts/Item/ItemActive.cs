using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemActive : Item {

    public GameObject target;
    // Use this for initialization
    protected override void Start()
    {

    }

    // Update is called once per frame
    protected override void Update()
    {

    }

    public abstract void Use();
}
