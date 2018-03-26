using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowWithTime : Attribute {
    public float time;
	// Use this for initialization
	void Start () {

        StartCoroutine(Explode());
	}

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(time);
        onDeath();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
