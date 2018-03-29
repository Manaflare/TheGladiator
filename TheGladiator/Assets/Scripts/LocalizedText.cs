using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LocalizedText : MonoBehaviour {

    private Text text_ui;
    public string keyValue;
	// Use this for initialization
	void Start () {
        text_ui = GetComponent<Text>();
        text_ui.text = Utility.GetLocalizedString(keyValue);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
