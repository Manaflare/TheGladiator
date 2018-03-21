using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this scripts is expected to be used for constant variables that we can have an access to this anywhere
public static class Constants
{
    //example
    public enum MoveType : int
    {
        ATTACK = 0,
        DODGE,
        MISS,
        DEATH
    }
    public enum AttributeTypes : int
    {
        HP = 0,
        STR,
        AGI,
        DEX,
        STA
    }
    public enum DayTimeType
    {
        MORNING = 0,
        AFTERNOON,
        NIGHT,
    }
    public enum SpriteType
    {
        BODY = 0,
        HAIR,
        ARMOR,
        FACIAL_HAIR,
        HELMET,
        RIGHT_HAND,
        LEFT_HAND,
        FOOT,
        PANTS
    }


    public enum DayType
    {
        MONDAY = 0,
        TUESDAY,
        WEDNESDAY,
        THURSDAY,
        FRIDAY,
        SATURDAY,
        SUNDAY,
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
        RIGHT_HAND = 0,
        ARMOR,
        LEFT_HAND,
        HELMET,
        PANTS,
        SHOES,
    }

    public enum PlayerType
    {
        PLAYER = 0,
        ENEMY,
        BOSS
    }

    public enum BuildingPanel_Type
    {
        NOT_IMPLEMENTED =0,
        WINDOW,
        SCENE,
    }

    public enum BuildingPanel_Status
    {
        NOT_AVAILABLE = 0,
        AVAILABLE,
    }

    public const float SECOND_FOR_DAY = 86400.0f;
    public const float TIME_DAYTIME = 43200.0f;
    public const float TIME_SUNSET = 64800.0f;
    public const float TIME_DAWN = 21600.0f;
    public const float PER_TIME = 21600.0f;


    public enum JSONIndex : int 
    {
        DATA_PLAYER = 1,
        DATA_ENEMY,
        DATA_ITEM,
        DATA_ENVIRONMENT,
    }

    public delegate void CallbackFunction();
    public delegate void CallbackFunctionWithArg1<T>(T arg);

    public enum PopupType
    {
        POPUP_SYSTEM,
        POPUP_YES,
        POPUP_NO,
    }
    public enum ENEMYTierIndex
    {
        TIER_1 = 0,
        TIER_2,
        TIER_3,

    }
    public static  float HOUR_SPENT = 8.0f;
    public static float STAMINA_REGEN_INTERVAL = 1.0f;

    public static string RIGHT_HAND = "right hand";
    public static string ARMOR = "armor";
    public static string LEFT_HAND = "left hand";
    public static string HELMET = "helmet";
    public static string PANTS = "pants";
    public static string SHOES = "shoes";

    public static string C_RIGHT_HAND = "Right Hand";
    public static string C_ARMOR = "Armor";
    public static string C_LEFT_HAND = "Left Hand";
    public static string C_HELMET = "Helmet";
    public static string C_PANTS = "Pants";
    public static string C_SHOES = "Shoes";

    public static string PLAYER_NAME = "Player";
    public static string ENEMY_NAME = "Player 2";

    public static string PLAYER1_TAG = "player1";
    public static string ENEMY_TAG = "player2";

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

    public static float MINIMUM_DODGE = 0.0f; //Added for customizablity
    public static float MAXIMUM_DODGE = 0.7F;
    public static float DODGE_STEP_AMOUNT = (MAXIMUM_DODGE - MINIMUM_DODGE) / MAX_STAT_LEVEL;

}

