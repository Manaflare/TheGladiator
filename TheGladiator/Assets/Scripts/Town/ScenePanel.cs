using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenePanel : MonoBehaviour
{
    private string sceneName;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetSceneName(string name)
    {
        sceneName = name;
    }

    public void OnGoBattle()
    {
        OnCancel();
        MasterManager.ManagerLoadScene.LoadScene(sceneName);
    }

    public void OnSkip()
    {
        OnCancel();
        DayNightCycleManager.Instance.SpendTime(Constants.TIME_GAMESTART, Constants.DayType.MONDAY, MasterManager.ManagerGlobalData.GetEnvData().weeks + 1);
    }

    public void OnCancel()
    {
        TownManager.Instance.CloseCurrentWindow(false);
    }
}
