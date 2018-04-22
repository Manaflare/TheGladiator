using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LocalizedText : MonoBehaviour
{
    public enum TEXT_TYPE
    {
        DEFAULT = 0,
        UPPER_CASE,
        LOWER_CASE,
    }
    private Text text_ui;
    public string keyValue;
    public TEXT_TYPE text_type = TEXT_TYPE.DEFAULT;
    // Use this for initialization
    void Start()
    {
        Intialize();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Intialize()
    {
        text_ui = GetComponent<Text>();
        string text = Utility.GetLocalizedString(keyValue);

        if (MasterManager.ManagerLocalize.GetLocaleType() == Constants.LOCALE_TYPE.EN_CA)
        {
            switch (text_type)
            {
                case TEXT_TYPE.DEFAULT:
                    break;
                case TEXT_TYPE.UPPER_CASE:
                    text = text.ToUpper();
                    break;
                case TEXT_TYPE.LOWER_CASE:
                    text = text.ToLower();
                    break;
                default:
                    break;
            }
        }


        text_ui.text = text;
    }

}
