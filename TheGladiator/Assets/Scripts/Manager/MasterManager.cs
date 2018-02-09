﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LoadSceneManager))]
[RequireComponent(typeof(GlobalDataManager))]
[RequireComponent(typeof(SoundManager))]
[RequireComponent(typeof(InputManager))]
public class MasterManager : MonoBehaviour {

    private List<IManager> managerList = new List<IManager>();

    public static LoadSceneManager  ManagerLoadScene    { get; private set; }
    public static GlobalDataManager ManagerGlobalData   { get; private set; }
    public static SoundManager      ManagerSound        { get; private set; }

    public static InputManager ManagerInput { get; private set; }


    private void Awake()
    {
        ManagerLoadScene = GetComponent<LoadSceneManager>();
        ManagerGlobalData = GetComponent<GlobalDataManager>();
        ManagerSound = GetComponent<SoundManager>();
        ManagerInput = GetComponent<InputManager>();

        //added all manager in the manager list
        managerList.Add(ManagerLoadScene);
        managerList.Add(ManagerGlobalData);
        managerList.Add(ManagerSound);
        managerList.Add(ManagerInput);

        StartCoroutine(IE_BootAllManager());

        //keep this gameobject the entire project
        DontDestroyOnLoad(gameObject);
    }

    IEnumerator IE_BootAllManager()
    {
        foreach(IManager manager in managerList)
        {
            manager.Initialize();
        }

        yield return null;
    }
}
