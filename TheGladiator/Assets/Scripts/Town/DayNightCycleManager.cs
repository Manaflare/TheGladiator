﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class EnvironmentData
{
    public long gold;
    public Constants.DayType days;
    public byte weeks = 1;
    public float times = Constants.TIME_GAMESTART; //game start at 8 am
    public byte house_level = 1;
}

public class DayNightCycleManager : MonoBehaviour
{

    private static DayNightCycleManager instance;
    public static DayNightCycleManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<DayNightCycleManager>();
                if (instance == null)
                {
                    GameObject container = new GameObject("DayNightCycleManager");
                    instance = container.AddComponent<DayNightCycleManager>();
                }
            }

            return instance;
        }
    }

    [SerializeField]
    private EnvironmentData envData;
    [SerializeField]
    private int speed = 10;
    [SerializeField]
    private int speedMutiplier = 3000;

    [SerializeField]
    private GameObject ClockImage;

    [SerializeField]
    private RuntimeAnimatorController[] imageAnimators;

    private TimeSpan currentTime;


    public Image TownImage;
    public Color fogDay = Color.gray;
    public Color fogSunset = Color.red;
    public Color fogNight = Color.black;

    public AnimationCurve curve;
    public float intensity = 0.0f;
    public Text uiText_Week;
    public Text uiText_Day;
    public Text uiText_RemainDays;
    // Use this for initialization

    private Color colourSource;
    private Color ColourDest;

    private bool speedUp = false;
    [SerializeField]
    private float expectingTime = 0.0f;
    private int expectingWeek = 0;
    private Constants.DayType expectingdDay;

    private Constants.CallbackFunction expectHandler;

    public GameObject Blocker;
    void Start()
    {
        //get time data from json
        //money, current time
        //set current data to UI
        envData = MasterManager.ManagerGlobalData.GetEnvData();

        if (MasterManager.ManagerLoadScene.AfterBattle == true)
        {
            MasterManager.ManagerLoadScene.AfterBattle = false;

            StartNextWeek();
            //After Battle goes to Monday 8 am 
            envData.times = Constants.TIME_GAMESTART;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //update
        UpdateTime();
        UpdateUI();
    }

    private void UpdateTime()
    {
        envData.times += Time.smoothDeltaTime * speed;
        if (speedUp)
        {
            if (envData.times >= expectingTime && envData.days == expectingdDay && envData.weeks == expectingWeek)
            {
                EndExpectedTime();
            }
        }

        if (envData.times > Constants.SECOND_FOR_DAY)
        {
            StartNextDay();
        }

        if ((int)envData.days > (int)Constants.DayType.SUNDAY)
        {
            StartNextWeek();
        }

        currentTime = TimeSpan.FromSeconds(envData.times);


        if (envData.times < Constants.TIME_DAWN)       // dawn
        {
            colourSource = fogNight;
            ColourDest = fogDay;
            intensity = envData.times / Constants.PER_TIME;
        }
        else if (envData.times >= Constants.TIME_DAWN && envData.times < Constants.TIME_DAYTIME) //dawn to daytime
        {
            colourSource = fogDay;
            ColourDest = fogDay;
            intensity = (envData.times - Constants.TIME_DAWN) / Constants.PER_TIME;
        }
        else if (envData.times >= 43200 && envData.times < Constants.TIME_SUNSET) // daytime to sunset
        {
            colourSource = fogDay;
            ColourDest = fogSunset;
            intensity = (envData.times - Constants.TIME_DAYTIME) / Constants.PER_TIME;
        }
        else  //sunset to dawn
        {
            colourSource = fogSunset;
            ColourDest = fogNight;
            intensity = (envData.times - Constants.TIME_SUNSET) / Constants.PER_TIME;
        }

        //intensity = 1 - ((43200 - time) / 43200 * -1);

        //TownImage.color =  Color.Lerp(fogNight, fogDay, intensity * intensity);
        TownImage.color = Color.Lerp(colourSource, ColourDest, intensity * intensity);
    }

    private void UpdateUI()
    {
        uiText_Week.text = envData.weeks.ToString();
        string[] tempTime = currentTime.ToString().Split(":"[0]);
        uiText_Day.text = envData.days.ToString()[0] + envData.days.ToString().Remove(0, 1).ToLower() + "\n " + tempTime[0] + ":" + tempTime[1];
        uiText_RemainDays.text = GetRemainDayForBattle().ToString();
    }

    public int GetRemainDayForBattle()
    {
        return (int)Constants.DayType.SUNDAY - (int)envData.days;
    }

    public bool IsDayForBattle()
    {
        return ((int)envData.days == (int)Constants.DayType.SUNDAY);
    }


    void StartNextDay()
    {
        envData.days++;
        envData.times = 0;

        if (IsDayForBattle())
        {
            MasterManager.ManagerPopup.ShowMessageBox("System", "Time to Fight", Constants.PopupType.POPUP_SYSTEM);
        }
    }


    void StartNextWeek()
    {
        envData.weeks++;
        envData.days = 0;
        if (MasterManager.ManagerGlobalData.GetPlayerDataInfo().playerTier <= Constants.MAX_ENEMY_RANK)
        {
            //popup message and
            MasterManager.ManagerPopup.ShowMessageBox("System", "Next Week Started", Constants.PopupType.POPUP_SYSTEM);
        }
        TownManager.Instance.WorkForNextWeek();
    }

    public void SpendTime(float times, Constants.DayType day, int week, Constants.CallbackFunction callbackFunc = null, Constants.ClockImageType clockType = Constants.ClockImageType.HOUR_GLASS)
    {
        speed *= speedMutiplier;
        speedUp = true;
        expectingTime = times;
        expectingdDay = day;
        expectingWeek = week;

        expectHandler = callbackFunc;

        Blocker.SetActive(true);
        ClockImage.SetActive(true);
        ClockImage.GetComponent<Animator>().runtimeAnimatorController = imageAnimators[(byte)clockType];
    }
    public void SpendTime(float hourMultiPlier = 1.0f, Constants.CallbackFunction callbackFunc = null, Constants.ClockImageType clockType = Constants.ClockImageType.HOUR_GLASS)
    {
        float calcTime = envData.times + (Constants.HOUR_SPENT * hourMultiPlier * 3600f);
        Constants.DayType calcDay = envData.days;
        int calcWeek = envData.weeks;

        for (int count = 1; calcTime >= Constants.SECOND_FOR_DAY; ++count)
        {
            calcTime -= Constants.SECOND_FOR_DAY;
            calcDay = envData.days + count;
            if (calcDay > Constants.DayType.SUNDAY)
            {
                calcDay = 0;
                calcWeek = envData.weeks + 1;
            }
        }

        SpendTime(calcTime, calcDay, calcWeek, callbackFunc, clockType);
    }

    private void EndExpectedTime()
    {
        Blocker.SetActive(false);
        ClockImage.SetActive(false);
        if (expectHandler != null)
        {
            expectHandler();
            expectHandler = null;
        }

        speedUp = false;
        speed = 10;
    }

}
