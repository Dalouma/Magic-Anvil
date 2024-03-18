using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioMixer mixer;
    public List<AudioSource> musicSources;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void StopAllMusic()
    {
        for (int i = 0; i < musicSources.Count; i++)
        {
            musicSources[i].Stop();
        }
    }

    public void ChangeMusic(string trackName)
    {
        StopAllMusic();
        if (trackName == "menu")
        {
            musicSources[0].Play();
        }
        if (trackName == "forge")
        {
            musicSources[1].Play();
        }
    }

    public void SetVolumeLevel (float sliderVal)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderVal) * 20);
    }
}
