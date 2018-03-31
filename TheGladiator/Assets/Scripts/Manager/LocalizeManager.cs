using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class LocalizeManager : MonoBehaviour, IManager {

    private Dictionary<string, string> stringList = new Dictionary<string, string>();
    private Constants.LOCALE_TYPE locale_type;
    // Use this for initialization

    public void Initialize()
    {
        stringList.Clear();

        StreamReader reader = new StreamReader(Constants.CONFIG_PATH + "locale.slc");
        string current_locale = reader.ReadLine().ToUpper();

        locale_type = (Constants.LOCALE_TYPE)System.Enum.Parse(typeof(Constants.LOCALE_TYPE), current_locale);

        //make name of the local file
        string locale_file_path = Constants.LOCALE_PATH;
        locale_file_path += locale_type.ToString().ToLower();
        locale_file_path += ".txt";

        StreamReader locale_reader = new StreamReader(locale_file_path);

        string line = locale_reader.ReadLine();
        while(line != null)
        {
            Parse(line);
            line = locale_reader.ReadLine();
        }

        string temp = GetValue("#TEXT_HOME_MESSAGE");
        Debug.Log(temp);

        locale_reader.Close();
        reader.Close();
    }

    public Constants.LOCALE_TYPE GetLocaleType()
    {
        return locale_type;
    }

    public void ChangeLanguage(Constants.LOCALE_TYPE type)
    {
        //overwrite
        StreamWriter writer = new StreamWriter(Constants.CONFIG_PATH + "locale.slc", false);
        writer.WriteLine(type.ToString().ToLower());
        writer.Close();

        //intialize
        Initialize();
    }

    private void Parse(string str)
    {
        //comment or empty line
        if (str == "" || str[0] == '$')
            return;

        //try to split string based on whitespace like space or tab
        string[] arr_string = str.Split(new char[0], System.StringSplitOptions.RemoveEmptyEntries);

        string value_string = "";

        //start from index 1 because first string is the key value
        for(int i = 1; i < arr_string.Length; ++i)
        {
            //check for special character;
            string checkedString = arr_string[i].Replace("\\n", "\n"); 
            value_string += checkedString;

            //ignore last space
            if(i < arr_string.Length - 1)
                value_string += " ";
        }

        stringList.Add(arr_string[0], value_string);        
    }

    public string GetValue(string keyValue)
    {
        if (keyValue[0] != '#')
            return null;

        string result;
        if(stringList.TryGetValue(keyValue, out result) == true)
        {
            return result;
        }
        else
        {
            Debug.LogWarning("the key (" + keyValue + ") doesn't exist. please check it again");
            return keyValue;
        }
    }
}
