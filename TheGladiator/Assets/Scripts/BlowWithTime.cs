using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowWithTime : Attribute {
    public float time;
	// Use this for initialization
	protected override void Start () {

        StartCoroutine(Explode());
	}

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(time);
        onDeath();

    }
}
