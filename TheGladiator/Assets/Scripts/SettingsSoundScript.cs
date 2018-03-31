using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SettingsSoundScript : MonoBehaviour
{
    private float MasterAudioLevel;
    private float BGMAudioLevel;
    private float SFXAudioLevel;

    private float initialMasterAudioLevel;
    private float initialBGMAudioLevel;
    private float initialSFXAudioLevel;

    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;

    public Dropdown languageDropdown;
    private Constants.LOCALE_TYPE current_localeType;

    private void OnEnable()
    {
        current_localeType = MasterManager.ManagerLocalize.GetLocaleType();
        initialMasterAudioLevel = MasterAudioLevel = PlayerPrefs.HasKey("MasterVolume") ? PlayerPrefs.GetFloat("MasterVolume") : 100;
        initialBGMAudioLevel =  BGMAudioLevel = PlayerPrefs.HasKey("BGMVolume") ? PlayerPrefs.GetFloat("BGMVolume") : 100;
        initialSFXAudioLevel = SFXAudioLevel = PlayerPrefs.HasKey("SFXVolume") ? PlayerPrefs.GetFloat("SFXVolume") : 100;

        masterSlider.value = MasterAudioLevel;
        bgmSlider.value = BGMAudioLevel;
        sfxSlider.value = SFXAudioLevel;

        InitializeLanguege();

        languageDropdown.value = (int)current_localeType;
    }
    void InitializeLanguege()
    {
        LocalizedText[] textsinSettings = GameObject.FindObjectsOfType<LocalizedText>();
        foreach(LocalizedText localText in textsinSettings)
        {
            localText.Intialize();
        }

        languageDropdown.ClearOptions();
        List<string> languageList = new List<string>();
        languageList.Add(Utility.GetLocalizedString("#TEXT_LOCALE_ENGLISH"));
        languageList.Add(Utility.GetLocalizedString("#TEXT_LOCALE_FRENCH"));
        languageList.Add(Utility.GetLocalizedString("#TEXT_LOCALE_KOREAN"));
        languageList.Add(Utility.GetLocalizedString("#TEXT_LOCALE_POURT"));

        languageDropdown.AddOptions(languageList);
    }
	// Update is called once per frame
	void Update ()
    {
		
	}
    public void onMasterVolumeChange()
    {
        MasterAudioLevel = masterSlider.value;
        //Debug.Log("Master changed to " + MasterAudioLevel);
        MasterManager.ManagerSound.PreviewAppliedSetting("MasterVolume", MasterAudioLevel);
    }
    public void onBGMVolumeChange()
    {
        BGMAudioLevel = bgmSlider.value;
        //Debug.Log("BGM changed to " + BGMAudioLevel);
        MasterManager.ManagerSound.PreviewAppliedSetting("BGMVolume", BGMAudioLevel);
    }
    public void onSFXVolumeChange()
    {
        SFXAudioLevel = sfxSlider.value;
        //Debug.Log("SFX changed to " + SFXAudioLevel);
        MasterManager.ManagerSound.PreviewAppliedSetting("SFXVolume", SFXAudioLevel);
    }
    public void saveChanges(bool isMainMenu = false)
    {
        PlayerPrefs.SetFloat("MasterVolume", MasterAudioLevel);
        PlayerPrefs.SetFloat("BGMVolume", BGMAudioLevel);
        PlayerPrefs.SetFloat("SFXVolume", SFXAudioLevel);
       
        closePopup(isMainMenu);
    }

    public void Back(bool isMainMenu = false)
    {
        //restore
        MasterManager.ManagerLocalize.ChangeLanguage(current_localeType);
        //change current window
        InitializeLanguege();

        PlayerPrefs.SetFloat("MasterVolume", initialMasterAudioLevel);
        PlayerPrefs.SetFloat("BGMVolume", initialBGMAudioLevel);
        PlayerPrefs.SetFloat("SFXVolume", initialSFXAudioLevel);
   
        closePopup(isMainMenu);
    }
    public void closePopup(bool isMainMenu = false)
    {
        if (isMainMenu)
        {
            this.transform.parent.gameObject.SetActive(false);

        }
        else
        {
            TownManager.Instance.CloseCurrentWindow(false);
        }

        //change current window
        InitializeLanguege();

    }

    public void OnChangeLanguage()
    {
        Constants.LOCALE_TYPE type = (Constants.LOCALE_TYPE)languageDropdown.value;
        MasterManager.ManagerLocalize.ChangeLanguage(type);
        //change current window
        InitializeLanguege();
    }
}
