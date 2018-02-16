﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNightCycleManager : MonoBehaviour {

    private static DayNightCycleManager instance;
    public static DayNightCycleManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<DayNightCycleManager>();
                if(instance == null)
                {
                    GameObject container = new GameObject("DayNightCycleManager");
                    instance = container.AddComponent<DayNightCycleManager>();
                }
            }

            return instance;
        }
    }

    [SerializeField]
    private float time;
    private Constants.DayType days;
    private int weeks;
    [SerializeField]
    private int speed = 2;
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
    void Start ()
    {
        //get time data from json
        //set current data to UI
    }
	
	// Update is called once per frame
	void Update ()
    {
        //update
        UpdateTime();
        UpdateUI();
    }

    private void UpdateTime()
    {
        time += Time.smoothDeltaTime * speed;
        if(time > Constants.SECOND_FOR_DAY)
        {
            days++;
            time = 0;
        }

        if((int)days > (int)Constants.DayType.SUNDAY)
        {
            weeks++;
            days = 0;
        }

        currentTime = TimeSpan.FromSeconds(time);

        
        if (time < Constants.TIME_DAWN)       // dawn
        {
            colourSource = fogNight;
            ColourDest = fogNight;
            intensity = 1;
        }
        else if(time >= Constants.TIME_DAWN && time < Constants.TIME_DAYTIME) //dawn to daytime
        {
            colourSource = fogNight;
            ColourDest = fogDay;
            intensity =  (time - Constants.TIME_DAWN) / Constants.PER_TIME;
        }
        else if (time >= 43200 && time < Constants.TIME_SUNSET) // daytime to sunset
        {
            colourSource = fogDay;
            ColourDest = fogSunset;
            intensity = (time - Constants.TIME_DAYTIME) / Constants.PER_TIME;
        }
        else  //sunset to dawn
        {
            colourSource = fogSunset;
            ColourDest = fogNight;
            intensity = (time - Constants.TIME_SUNSET) / Constants.PER_TIME;
        }
            
        //intensity = 1 - ((43200 - time) / 43200 * -1);

        //TownImage.color =  Color.Lerp(fogNight, fogDay, intensity * intensity);
        TownImage.color = Color.Lerp(colourSource, ColourDest, intensity * intensity);
    }

    private void UpdateUI()
    {
        uiText_Week.text = weeks.ToString();
        string[] tempTime = currentTime.ToString().Split(":"[0]);
        uiText_Day.text = days.ToString() + ": " + tempTime[0] + ":" + tempTime[1];
        uiText_RemainDays.text = GetRemainDayForBattle().ToString();
    }

    public int GetRemainDayForBattle()
    {
        return (int)Constants.DayType.SUNDAY - (int)days;
    }

    public bool IsDayForBattle()
    {
        return ((int)days == (int)Constants.DayType.SUNDAY);
    }


}
