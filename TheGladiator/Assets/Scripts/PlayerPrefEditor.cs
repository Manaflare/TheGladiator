using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerPrefEditor : EditorWindow {

    enum PlayerPrefsValueType
    {
        Unknown,
        String,
        Float,
        Int
    }

    class TrySetResponse
    {

        public bool success { get; set; }
        public string message { get; set; }
        public MessageType messageType { get; set; }

        public void SetValues(string message, bool success, MessageType messageType)
        {
            this.message = message;
            this.success = success;
            this.messageType = messageType;
        }
    }


    // Search vars
    private string searchKey;
    private string searchNewValue;
    private PlayerPrefsValueType searchValType_m = PlayerPrefsValueType.String;
    private TrySetResponse searchResponse;

    // Add key vars
    private string addKey;
    private string addValue;
    private PlayerPrefsValueType addValueType = PlayerPrefsValueType.String;
    private TrySetResponse addResponse;

    [MenuItem("Global Data/Open editor")]
    static void Init()
    {
        PlayerPrefEditor editorWindow = (PlayerPrefEditor)GetWindow(typeof(PlayerPrefEditor), true, "Player prefs editor");
        editorWindow.Show();
    }

    void OnGUI()
    {
        DrawSearchKey();
        DrawAddKey();
    }

    private void DrawSearchKey()
    {
        GUILayout.Label("Search for key", EditorStyles.boldLabel);
        searchKey = EditorGUILayout.TextField("Key", searchKey);

        //Edit existing key
        if (PlayerPrefs.HasKey(searchKey))
        {

            PlayerPrefsValueType type = GetType(searchKey);

            // Delete
            if (GUILayout.Button("Delete"))
            {
                PlayerPrefs.DeleteKey(searchKey);
                Debug.Log("PlayerPrefs key: " + searchKey + ", deleted");
            }

            if (type == PlayerPrefsValueType.Unknown)
            {
                searchValType_m = (PlayerPrefsValueType)EditorGUILayout.EnumPopup("Type", searchValType_m);
                EditorGUILayout.HelpBox("The value for the key is a default value so the type cannot be determined. It is your responsibility to set the value in correct type.", MessageType.Warning);
            }
            else
            {
                searchValType_m = type;
                GUILayout.Label("Value type: " + searchValType_m, EditorStyles.boldLabel);
                GUILayout.Label("Current value: " + GetValue(searchKey, searchValType_m), EditorStyles.boldLabel);
            }

            // Set value
            GUILayout.Label("Set new value", EditorStyles.boldLabel);
            searchNewValue = EditorGUILayout.TextField("New value", searchNewValue);


            if (GUILayout.Button("Set"))
            {
                searchResponse = TrySetValue(searchKey, searchNewValue, searchValType_m);
            }
            if (searchResponse != null)
            {
                EditorGUILayout.HelpBox(searchResponse.message, searchResponse.messageType);
            }

          

        }
        else
        {
            EditorGUILayout.HelpBox("Key doesn't exist in player prefs", MessageType.Warning);
        }
    }

    private void DrawAddKey()
    {
        GUILayout.Label("Add key", EditorStyles.boldLabel);
        addKey = EditorGUILayout.TextField("Key", addKey);
        addValue = EditorGUILayout.TextField("Value", addValue);
        addValueType = (PlayerPrefsValueType)EditorGUILayout.EnumPopup("Type", addValueType);
        if (GUILayout.Button("Add"))
        {
            addResponse = TrySetValue(addKey, addValue, addValueType);
        }
        if (addResponse != null)
        {
            EditorGUILayout.HelpBox(addResponse.message, addResponse.messageType);
        }
    }

    private static PlayerPrefsValueType GetType(string key)
    {
        if (!PlayerPrefs.HasKey(key)) throw new ArgumentException("Key didn't exist in PlayerPrefs");
        PlayerPrefsValueType type = PlayerPrefsValueType.Unknown;

        float floatVal = PlayerPrefs.GetFloat(key);
        int intVal = PlayerPrefs.GetInt(key);
        string stringVal = PlayerPrefs.GetString(key);

        if (floatVal == (default(float)) && intVal == (default(int)) && !stringVal.Equals(string.Empty))
        {
            type = PlayerPrefsValueType.String;
        }
        else if (floatVal == (default(float)) && intVal != (default(int)) && stringVal.Equals(string.Empty))
        {
            type = PlayerPrefsValueType.Int;
        }
        else if (floatVal != (default(float)) && intVal == (default(int)) && stringVal.Equals(string.Empty))
        {
            type = PlayerPrefsValueType.Float;
        }
        return type;
    }

    private static TrySetResponse TrySetValue(string key, string value, PlayerPrefsValueType type)
    {
        TrySetResponse response = new TrySetResponse()
        {
            message = "Key: " + key + " with Value: " + value + " was successfully saved to PlayerPrefs as a " + type,
            success = true,
            messageType = MessageType.Info
        };
        switch (type)
        {
            case PlayerPrefsValueType.String:
                PlayerPrefs.SetString(key, value);
                PlayerPrefs.Save();
                break;
            case PlayerPrefsValueType.Float:
                float newValFloat;
                if (float.TryParse(value, out newValFloat))
                {
                    PlayerPrefs.SetFloat(key, newValFloat);
                    PlayerPrefs.Save();
                }
                else
                {
                    response.SetValues("Couldn't parse input value:" + value + " to target type float. Input a valid float value.", false, MessageType.Error);
                }
                break;
            case PlayerPrefsValueType.Int:
                int newValInt;
                if (int.TryParse(value, out newValInt))
                {
                    PlayerPrefs.SetInt(key, newValInt);
                    PlayerPrefs.Save();
                }
                else
                {
                    response.SetValues("Couldn't parse input value:" + value + " to target type int. Input a valid int value.", false, MessageType.Error);
                }
                break;
            default:
                response.SetValues("Unknown PlayerPrefsValueType: " + type, false, MessageType.Error);
                break;
        }
        return response;
    }

    private string GetValue(string key, PlayerPrefsValueType type)
    {
        if (!PlayerPrefs.HasKey(key)) throw new ArgumentException("Key didn't exist in PlayerPrefs");
        switch (type)
        {
            case PlayerPrefsValueType.String:
                {
                    return PlayerPrefs.GetString(key);
                }
            case PlayerPrefsValueType.Float:
                {
                    return PlayerPrefs.GetFloat(key).ToString();
                }
            case PlayerPrefsValueType.Int:
                {
                    return PlayerPrefs.GetInt(key).ToString();
                }
            default:
                {
                    throw new ArgumentOutOfRangeException("Unknown Type");
                }

        }
    }

    [MenuItem("Global Data/Save All")]
    static void SaveAll()
    {
        PlayerPrefs.Save();
        Debug.Log("All global data is saved.");
    }

    [MenuItem("Global Data/Delete All")]
    static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("All global data is deleted.");
    }
}
