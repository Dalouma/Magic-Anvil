using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioMixer mixer;
    public List<AudioSource> musicSources;
    private string currPlaying = "menu";

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

    public void ChangeMusic(string trackName, bool restart = false)
    {
        // If the music to change to is the same as the music currently playing, only change if specified
        if(!restart && trackName == currPlaying) {
            Debug.Log("Already Playing");
            return;
        }

        // Track is different or restart was specified. Stop music and start a new track
        StopAllMusic();
        currPlaying = trackName;
        Debug.Log($"Now Playing {trackName}");
        switch (trackName) {
            case "menu":
                musicSources[0].Play();
                break;
            case "forge":
                musicSources[1].Play();
                break;
            case "robbery":
                musicSources[2].Play();
                break;
            default:
                Debug.LogError($"Error: Invalid track name {trackName}");
                break;
        }
    }

    public void SetVolumeLevel(float sliderVal)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderVal) * 20);
    }
}
