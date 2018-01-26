using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour, IManager {

    public AudioClip[] ac;
    public AudioSource musicSource;
    public AudioSource sfxSource;

    public float lowPitch = 0.95f;
    public float HighPitch = 1.05f;


    private Dictionary<string, AudioClip> mapAudioFiles;

    // Use this for initialization
    public void Initialize()
    {
        mapAudioFiles = new Dictionary<string, AudioClip>();

        foreach (AudioClip audio in ac)
        {
            mapAudioFiles.Add(audio.name, audio);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void PlayBackgroundMusic(AudioClip backgroundMusic)
    {
        if (musicSource.isPlaying == true)
            musicSource.Stop();

        musicSource.clip = backgroundMusic;
        musicSource.Play();
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
            throw new System.Exception(audioName + " is not in the auidolist in soundManager");
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
        sfxSource.pitch = UnityEngine.Random.Range(lowPitch, HighPitch);
        int randomIdx = UnityEngine.Random.Range(0, clips.Length);

        sfxSource.PlayOneShot(clips[randomIdx]);
    }
}
