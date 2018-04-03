using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditText : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    private float speed = 1.5f;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        RectTransform rt = GetComponent<RectTransform>();
        if(rt.anchoredPosition.y <= -20)
           GetComponent<RectTransform>().anchoredPosition = new Vector2(rt.anchoredPosition.x, rt.anchoredPosition.y + speed);
        else
        {

        }
	}

}
