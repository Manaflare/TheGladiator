using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SoundManager : MonoBehaviour, IManager {

    public AudioClip[] ac;
    public AudioMixer masterMixer;
    public AudioSource musicSource;
    public AudioSource sfxSource;

    public float lowPitch = 0.95f;
    public float HighPitch = 1.05f;

    [SerializeField]
    private Dictionary<string, AudioClip> mapAudioFiles;

    // Use this for initialization
    public void Initialize()
    {
        mapAudioFiles = new Dictionary<string, AudioClip>();

        foreach (AudioClip audio in ac)
        {
            mapAudioFiles.Add(audio.name, audio);
        }

        Debug.Log("boot Done " + typeof(SoundManager));
        ApplyToAllSettings();
    }

    // Update is called once per frame
    void Update () {

	}

    public void PlayBackgroundMusic(AudioClip backgroundMusic, bool looping = true)
    {
        if(musicSource == null)
        {
            return;
        }
        if (musicSource.isPlaying == true)
            musicSource.Stop();

        musicSource.loop = looping;

        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    public void StopBackgroundMusic()
    {
        if (musicSource.isPlaying == true)
            musicSource.Stop();
    }

    public void ChangeBackgroundMusic(AudioClip music)
    {
        PlayBackgroundMusic(music);
    }

    public void PlaySingleSound(string audioName)
    {
        AudioClip playableSound;
        if (mapAudioFiles.TryGetValue(audioName, out playableSound) == true)
        {
            PlaySingleSound(playableSound);
        }
        else
        {
         //   throw new System.Exception(audioName + " is not in the auidolist in soundManager");
        }
    }

    public void PlayRandomSound(params string[] audioNames)
    {
        List<AudioClip> playableSounds = new List<AudioClip>();
        foreach (string audioName in audioNames)
        {
            AudioClip playableSound;
            if (mapAudioFiles.TryGetValue(audioName, out playableSound) == true)
            {
                playableSounds.Add(playableSound);
            }
            else
            {
                throw new System.Exception(audioName + " is not in the auidolist in soundManager");
            }

        }

        PlayRandomSound(playableSounds.ToArray());
    }

    public void PlaySingleSound(AudioClip clip, float vol = 1.0f)
    {
        sfxSource.PlayOneShot(clip, vol);
    }

    public void PlayRandomSound(params AudioClip[] clips)
    {
        float randPitch = UnityEngine.Random.Range(lowPitch, HighPitch);
        sfxSource.pitch = randPitch;
        masterMixer.SetFloat("SFXPitch", randPitch);
        int randomIdx = UnityEngine.Random.Range(0, clips.Length);

        sfxSource.PlayOneShot(clips[randomIdx]);
    }

    public void ApplyToAllSettings()
    {
        if(masterMixer != null)
        {
            ApplyToSetting("MasterVolume");
            ApplyToSetting("BGMVolume");
            ApplyToSetting("SFXVolume");
        }
        
    }

    public void PreviewAppliedSetting(string keyExposedParam, float sliderValue)
    {
        float valueForMixer = Mathf.Log10(sliderValue);
        float normalizedValue = Mathf.Lerp(-80f, 0f, valueForMixer - 1);

        masterMixer.SetFloat(keyExposedParam, normalizedValue);
    }

    public void ApplyToSetting(string keyValue)
    {
        const float default_value = 100.0f;

        float VolumeValue = PlayerPrefs.GetFloat(keyValue, default_value);
        float valueForMixer = Mathf.Log10(VolumeValue);
        float normalizedValue = Mathf.Lerp(-80f, 0f, valueForMixer - 1);

        //Debug.Log("SliderValue : " + VolumeValue + " MixerValue : " + valueForMixer + " rangedValue : " + normalizedValue);
        masterMixer.SetFloat(keyValue, normalizedValue);
    }
}
