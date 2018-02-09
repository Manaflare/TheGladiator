using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttribute : Attribute
{
    public bool IsVulnerable { get; private set; }
	// Use this for initialization
	protected override void Start ()
    {
        
	}
    public override void onDeath()
    {
        base.onDeath();
    }
    // Update is called once per frame
    protected override void Update ()
    {
		
	}
}
