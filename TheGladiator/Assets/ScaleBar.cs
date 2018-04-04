using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBar : MonoBehaviour {

    public GameObject bar;
    RectTransform barRect;
    
    float initWidth; 
	// Use this for initialization
	void Awake() {
        barRect = bar.GetComponent<RectTransform>();
        initWidth = barRect.offsetMax.x;
        Debug.Log(barRect.offsetMax.x);
	}
    
    public void UpdateBar(Stats s)
    {
        if (s == null)
            return;

        barRect.offsetMax = new Vector2(initWidth - (300 - (300 * s.HP/ (s.MAXHP * 5))) , barRect.offsetMax.y);   
    }

}
