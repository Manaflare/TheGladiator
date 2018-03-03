using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this scripts is expected to be used for common function that we can have an access to this anywhere
public static class Utility
{
    public static string getStringFromName(Constants.PlayerType n)
    {
        return (n == 0) ? Constants.playerName : Constants.enemyName;
    }

    //example
    static Dictionary<Constants.JSONIndex, string> JsonFileList = new Dictionary<Constants.JSONIndex, string>()
    {
        {Constants.JSONIndex.DATA_PLAYER,       "/JSON/playerData.json"},
        {Constants.JSONIndex.DATA_ENEMY_TIER_1, "/JSON/tier1Enemies.json"},
        {Constants.JSONIndex.DATA_ENEMY_TIER_2, "/JSON/tier2Enemies.json"},
        {Constants.JSONIndex.DATA_ENEMY_TIER_3, "/JSON/tier3Enemies.json"},
    };


    public static ListDataInfo ReadDataInfoFromJSON(Constants.JSONIndex fileIndex)
    {
        string fileName;
        if(JsonFileList.TryGetValue(fileIndex, out fileName) == true)
        {
            string jsonString = System.IO.File.ReadAllText(Application.dataPath + fileName);
            return JsonUtility.FromJson<ListDataInfo>(jsonString);
        }
        else
        {
            throw new System.Exception("There is no fileNumber in the list");
        }
        
       
    }

    public static void WriteDataInfoToJSON(Constants.JSONIndex fileIndex, ref ListDataInfo status)
    {
        string fileName;
        if (JsonFileList.TryGetValue(fileIndex, out fileName) == true)
        {
            string jsonString = JsonUtility.ToJson(status);
            System.IO.File.WriteAllText(Application.dataPath + fileName, jsonString);
        }
        else
        {
            throw new System.Exception("There is no fileNumber in the list");
        }

        

    }


    public static string ConvertString(int number)
    {
        return number.ToString();
    }
}
