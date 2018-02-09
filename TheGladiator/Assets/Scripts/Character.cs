using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public bool isAttacking = false;

    public void startAttack()
    {
        isAttacking = true;
    }
    public void endAttack()
    {
        isAttacking = false;
    }
	// Use this for initialization
	protected virtual void Start ()
    {
		
	}

    // Update is called once per frame
    protected virtual void Update ()
    {
		
	}
}
