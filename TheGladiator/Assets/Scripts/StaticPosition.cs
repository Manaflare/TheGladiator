using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticPosition : MonoBehaviour {
    Transform original;
    // Use this for initialization
    void Start()
    {
        
        original = this.GetComponentInParent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(original.localPosition);
        Debug.Log(this.transform.localPosition.y);
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, original.localPosition.y, original.localPosition.z);
        this.transform.rotation = Quaternion.identity;
    }
}
