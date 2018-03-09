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
        MasterAudioLevel = PlayerPrefs.GetFloat("MasterVolume");
        BGMAudioLevel = PlayerPrefs.GetFloat("BGMVolume");
        SFXAudioLevel = PlayerPrefs.GetFloat("SFXVolume");

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
    }
    public void onBGMVolumeChange()
    {
        BGMAudioLevel = bgmSlider.value;
        Debug.Log("BGM changed to " + BGMAudioLevel);
    }
    public void onSFXVolumeChange()
    {
        SFXAudioLevel = sfxSlider.value;
        Debug.Log("SFX changed to " + SFXAudioLevel);
    }
    public void saveChanges()
    {
        PlayerPrefs.SetFloat("MasterVolume", MasterAudioLevel);
        PlayerPrefs.SetFloat("BGMVolume", BGMAudioLevel);
        PlayerPrefs.SetFloat("SFXVolume", SFXAudioLevel);
    }
}
