using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this scripts is expected to be used for common function that we can have an access to this anywhere
public static class Utility
{
    //example
    static Dictionary<int, string> JsonFileList = new Dictionary<int, string>()
    {
        {1, "/JSON/Data.json"},
    };


    public static ListStatus GetStatusFromJSON(int fileNumber)
    {
        string fileName;
        if(JsonFileList.TryGetValue(fileNumber, out fileName) == true)
        {
            string jsonString = System.IO.File.ReadAllText(Application.dataPath + fileName);
            return JsonUtility.FromJson<ListStatus>(jsonString);
        }

        throw new System.Exception("There is no fileNumber in the list");
       
    }

    public static string ConvertString(int number)
    {
        return number.ToString();
    }
}
