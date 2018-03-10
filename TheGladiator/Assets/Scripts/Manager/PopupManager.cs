using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour, IManager {

    public GameObject popupPrefabs;
    private GameObject popupWindow;

  
    // Use this for initialization
    public void Initialize()
    {

    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    public void ShowMessageBox(string title, string content, Constants.PopupType type, Constants.CallbackFunction func = null)
    {
        popupWindow = Instantiate(popupPrefabs);
        popupWindow.GetComponent<PopUp>().ShowMessageBox(title, content, type, func);
    }

    public void ShowMessageBox(string title, string content, Constants.PopupType type, Constants.CallbackFunctionWithArg1<object[]> func, object[] argues)
    {
        popupWindow = Instantiate(popupPrefabs);
        popupWindow.GetComponent<PopUp>().ShowMessageBox(title, content, type, func, argues);
    }

    public void CloseMessageBox()
    {
        popupWindow.GetComponent<PopUp>().CloseShowMessageBox();
    }

}
