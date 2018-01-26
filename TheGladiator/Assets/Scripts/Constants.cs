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

    public enum ItemIndex
    {
        WEAPON =0,
        ARMOR,
    }

    public static string playerName = "Hello Player";
    public static int PlayerIndex = 0;
}

