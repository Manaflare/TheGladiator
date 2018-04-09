using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemMenuPanel : MonoBehaviour {

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {

    }

    public void OnOK()
    {
        //  sleepImage.SetActive(true);
        TownManager.Instance.OnGoBackToMainMenu();
    }

    public void OnCancel()
    {
        TownManager.Instance.CloseCurrentWindow(false);
    }
}
