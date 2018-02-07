using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this scripts is expected to be used for constant variables that we can have an access to this anywhere
public static class Constants
{
    //example

    public enum DayTimeType
    {
        MORNING = 0,
        AFTERNOON,
        NIGHT,
    }
    public enum ArmorIndex
    {
        HELMET = 0,
        UPPER_BODY,
        LOWER_BODY,
        SHIELDS,
    }

    public enum HouseType
    {
        SMALL = 0,
        MEDIUM,
        HUGE,
        MANSION,
    }

    public enum ItemIndex
    {
        WEAPON =0,
        ARMOR,
    }

    public static string playerName = "Hello Player";
    public static int PlayerIndex = 0;



    public static string KEY_STRENGTH = "Strength";
    public static string KEY_AGILITY = "Agility";
    public static string KEY_DEXTERITY = "Dexterity";
    public static string KEY_STAMINA = "Stamina";

    public static byte MAX_STAT_LEVEL = byte.MaxValue;

    public static float MINIMUM_DELAY = 0.5f;
    public static float BASE_TIME = 2.0f;
    public static float MODIFIABLE_TIME = BASE_TIME - MINIMUM_DELAY;


    public static float MINIMUM_ACCURACY = 0.5f;
    public static float MAXIMUM_ACCURACY = 0.95f;
    public static float ACCURACY_STEP_AMOUNT = (MAXIMUM_ACCURACY - MINIMUM_ACCURACY) / MAX_STAT_LEVEL;

}

