using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEngine;

//this scripts is expected to be used for common function that we can have an access to this anywhere
public static class Utility
{
    public static string getStringFromName(Constants.PlayerType n)
    {
        return (n == 0) ? Constants.PLAYER_NAME : Constants.ENEMY_NAME;
    }

    //example
   public static Dictionary<Constants.JSONIndex, string> JsonFileList = new Dictionary<Constants.JSONIndex, string>()
    {
        {Constants.JSONIndex.DATA_PLAYER,       "/Resources/JSON/playerData.json"},
        {Constants.JSONIndex.DATA_ENEMY,        "/Resources/JSON/EnemyData.json" },
        {Constants.JSONIndex.DATA_ITEM,         "/Resources/JSON/ItemData.json" },
        {Constants.JSONIndex.DATA_ENVIRONMENT,  "/Resources/JSON/EnvData.json" },
        {Constants.JSONIndex.DATA_WORK,         "/Resources/JSON/WorkData.json" },
        {Constants.JSONIndex.DATA_CONFIG,       "/Resources/JSON/Config.json" },
        {Constants.JSONIndex.DATA_CREDIT,       "/Resources/JSON/Credit.json" },
    };

    public static void writeListToFile(List<Move> moves)
    {
        string fn = "BattleLog.txt";
        if (File.Exists(fn))
        {
            File.Delete(fn);
        }
        List<string> lines = new List<string>();
        foreach (Move m in moves)
        {
            lines.Add("=======================================[" + m.typeOfMove + "]============================================================");
            lines.Add("Delay time: " + m.delayTime);
            lines.Add(
                "Attacker Name: " + m.AttackerStats.PlayerType + " HP: " + m.AttackerStats.HP + "/" + m.AttackerStats.MAXHP +
                " Strength: " + m.AttackerStats.Strength + " Agility: " + m.AttackerStats.Agility + " Dexterity " + m.AttackerStats.Dexterity +
                " Stamina: " + m.AttackerStats.Stamina + "/" + m.AttackerStats.MaxStamina
                );
            lines.Add(
                 "Defender Name: " + m.DefenderStats.PlayerType + " HP: " + m.DefenderStats.HP + "/" + m.DefenderStats.MAXHP +
                 " Strength: " + m.DefenderStats.Strength + " Agility: " + m.DefenderStats.Agility + " Dexterity " + m.DefenderStats.Dexterity +
                 " Stamina: " + m.DefenderStats.Stamina + "/" + m.DefenderStats.MaxStamina
                 );

        }

        Process.Start("notepad.exe", fn);
        File.WriteAllLines(fn, lines.ToArray());

    }

    public static void writeToLog(string message, string outputfile = "ErrorLog.txt", bool open = true)
    {
        if (!File.Exists(outputfile))
            File.Create(outputfile);

        using (FileStream fs = File.Open(outputfile, FileMode.Append))
        {
            byte[] byteStr = Encoding.ASCII.GetBytes(message);
            fs.Write(byteStr, 0, byteStr.Length);
        }
    }

    public static T ReadDataFromJSON<T>(Constants.JSONIndex fileIndex)
    {
        string fileName;
        if(JsonFileList.TryGetValue(fileIndex, out fileName) == true)
        {
            string path = (Application.platform == RuntimePlatform.Android ||
               Application.platform == RuntimePlatform.IPhonePlayer ?
               Application.persistentDataPath : Application.dataPath) + fileName;

            if (!System.IO.File.Exists(path))
            {
                string tempJsonString = JsonUtility.ToJson("{ }");
                WriteDataToJSON(fileIndex, ref tempJsonString);
            }

            string jsonString = System.IO.File.ReadAllText(path);
            return JsonUtility.FromJson<T>(jsonString);
        }
        else
        {
            throw new System.Exception("There is no fileNumber in the list");
        }
        
       
    }

    public static void WriteDataToJSON<T>(Constants.JSONIndex fileIndex, ref T jsonData)
    {
        string fileName;
        if (JsonFileList.TryGetValue(fileIndex, out fileName) == true)
        {
            string jsonString = JsonUtility.ToJson(jsonData);
            string directory = (Application.platform == RuntimePlatform.Android ||
                Application.platform == RuntimePlatform.IPhonePlayer ?
                Application.persistentDataPath : Application.dataPath);
            string path = directory + fileName;

            if (!System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }
            string jsonFolder = '/' + fileName.Split('/')[1];
            if (!System.IO.Directory.Exists(directory + jsonFolder))
            {
                System.IO.Directory.CreateDirectory(directory + jsonFolder);
            }
            jsonFolder = '/' + fileName.Split('/')[1] + '/' + fileName.Split('/')[2];
            if (!System.IO.Directory.Exists(directory + jsonFolder))
            {
                System.IO.Directory.CreateDirectory(directory + jsonFolder);
            }

            if (!System.IO.File.Exists(path))
            {
                System.IO.File.Create(path).Dispose();
            }
            System.IO.File.WriteAllText(path, jsonString);
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


    public static string GetLocalizedString(string keyValue)
    {
        return MasterManager.ManagerLocalize.GetValue(keyValue);
    }
}
