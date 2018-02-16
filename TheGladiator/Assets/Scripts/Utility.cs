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
    static Dictionary<int, string> JsonFileList = new Dictionary<int, string>()
    {
        {1, "/JSON/Data.json"},
        {2, "/JSON/tier1Enemies.json"}
    };


    public static ListStatus ReadStatsFromJSON(int fileNumber)
    {
        string fileName;
        if(JsonFileList.TryGetValue(fileNumber, out fileName) == true)
        {
            string jsonString = System.IO.File.ReadAllText(Application.dataPath + fileName);
            return JsonUtility.FromJson<ListStatus>(jsonString);
        }
        else
        {
            throw new System.Exception("There is no fileNumber in the list");
        }
        
       
    }

    public static void WriteStatsToJSON(int fileNumber, ref ListStatus status)
    {
        string fileName;
        if (JsonFileList.TryGetValue(fileNumber, out fileName) == true)
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
