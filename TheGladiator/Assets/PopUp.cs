using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PopUp : MonoBehaviour {

    public Sprite[] sprites;

    public Image image_type;
    public Text text_title;
    public Text text_content;

    private Constants.CallbackFunction handler;
    private Constants.CallbackFunctionWithArg1<int> handlerInt;

    // Use this for initialization
    void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}
    private void SetUpData(string title, string content, Constants.PopupType type)
    {
        image_type.sprite = sprites[(int)type];
        text_title.text = title;
        text_content.text = content;
    }


    public void ShowMessageBox(string title, string content, Constants.PopupType type, Constants.CallbackFunction func)
    {
        SetUpData(title, content, type);
        handler = func;
    }

    public void ShowMessageBox(string title, string content, Constants.PopupType type, Constants.CallbackFunctionWithArg1<int> func)
    {
        SetUpData(title, content, type);
        handlerInt = func;
    }

    public void CloseShowMessageBox()
    {
        if(handler != null)
        {
            handler();
        }
     
        Destroy(gameObject);
    }
}
