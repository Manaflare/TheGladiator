using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class EnvironmentData
{
    public long gold;
    public Constants.DayType days;
    public byte weeks;
    public float times;
}

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
    private EnvironmentData envData;
    [SerializeField]
    private int speed = 10;
    [SerializeField]
    private int speedMutiplier = 3000;
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
    void Start ()
    {
        //get time data from json
        //money, current time
        //set current data to UI
        envData = MasterManager.ManagerGlobalData.GetEnvData();
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
        envData.times += Time.smoothDeltaTime * speed;
        if (speedUp)
        {
            if (envData.times >= expectingTime && envData.days == expectingdDay && envData.weeks == expectingWeek)
            {
                if(expectHandler != null)
                {
                    expectHandler();
                    expectHandler = null;
                }

                speedUp = false;
                speed = 10;
            }
        }
        
        if (envData.times > Constants.SECOND_FOR_DAY)
        {
            StartNextDay();
        }

        if((int)envData.days > (int)Constants.DayType.SUNDAY)
        {
            StartNextWeek();
        }

        currentTime = TimeSpan.FromSeconds(envData.times);

        
        if (envData.times < Constants.TIME_DAWN)       // dawn
        {
            colourSource = fogNight;
            ColourDest = fogNight;
            intensity = 1;
        }
        else if (envData.times >= Constants.TIME_DAWN && envData.times < Constants.TIME_DAYTIME) //dawn to daytime
        {
            colourSource = fogNight;
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
        uiText_Day.text = envData.days.ToString() + ": " + tempTime[0] + ":" + tempTime[1];
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

        if(IsDayForBattle())
        {
            MasterManager.ManagerPopup.ShowMessageBox("System", "Time to Fight", Constants.PopupType.POPUP_SYSTEM);
        }
    }


    void StartNextWeek()
    {
        envData.weeks++;
        envData.days = 0;

        //popup message and
        MasterManager.ManagerPopup.ShowMessageBox("System", "Next Week Started", Constants.PopupType.POPUP_SYSTEM);
    }

    public void SpendTime(float hourMultiPlier = 1.0f, Constants.CallbackFunction callbackFunc = null)
    {
        speed *= speedMutiplier;
        speedUp = true;
        expectingTime = envData.times + (Constants.HOUR_SPENT * hourMultiPlier * 3600f);
        expectingdDay = envData.days;
        expectingWeek = envData.weeks;

        expectHandler = callbackFunc;

        if (expectingTime >= Constants.SECOND_FOR_DAY)
        {
            expectingTime -= Constants.SECOND_FOR_DAY;
            expectingdDay = envData.days + 1;
            if(expectingdDay > Constants.DayType.SUNDAY)
            {
                expectingdDay = 0;
                expectingWeek = envData.weeks + 1;
            }
        }
            
    }

}
