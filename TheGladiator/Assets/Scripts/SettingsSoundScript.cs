using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SettingsSoundScript : MonoBehaviour
{
    private float MasterAudioLevel;
    private float BGMAudioLevel;
    private float SFXAudioLevel;

    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;

	// Use this for initialization
	void Start ()
    {
        MasterAudioLevel = PlayerPrefs.HasKey("MasterVolume") ? PlayerPrefs.GetFloat("MasterVolume"):100;
        BGMAudioLevel = PlayerPrefs.HasKey("BGMVolume") ? PlayerPrefs.GetFloat("BGMVolume") : 100;
        SFXAudioLevel = PlayerPrefs.HasKey("SFXVolume") ? PlayerPrefs.GetFloat("SFXVolume") : 100;

        masterSlider.value = MasterAudioLevel;
        bgmSlider.value = BGMAudioLevel;
        sfxSlider.value = SFXAudioLevel;
	}	
	// Update is called once per frame
	void Update ()
    {
		
	}
    public void onMasterVolumeChange()
    {
        MasterAudioLevel = masterSlider.value;
        Debug.Log("Master changed to " + MasterAudioLevel);
        MasterManager.ManagerSound.PreviewAppliedSetting("MasterVolume", MasterAudioLevel);
    }
    public void onBGMVolumeChange()
    {
        BGMAudioLevel = bgmSlider.value;
        Debug.Log("BGM changed to " + BGMAudioLevel);
        MasterManager.ManagerSound.PreviewAppliedSetting("BGMVolume", BGMAudioLevel);
    }
    public void onSFXVolumeChange()
    {
        SFXAudioLevel = sfxSlider.value;
        Debug.Log("SFX changed to " + SFXAudioLevel);
        MasterManager.ManagerSound.PreviewAppliedSetting("SFXVolume", SFXAudioLevel);
    }
    public void saveChanges(bool isMainMenu = false)
    {
        PlayerPrefs.SetFloat("MasterVolume", MasterAudioLevel);
        PlayerPrefs.SetFloat("BGMVolume", BGMAudioLevel);
        PlayerPrefs.SetFloat("SFXVolume", SFXAudioLevel);
       
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
    }
}
