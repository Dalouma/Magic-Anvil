using System;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioMixer mixer;
    public List<AudioSource> musicSources;
    public List<AudioSource> sfxSources;
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

    // Music Handling

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

    // SFX handling
    // Get the audio source corresponding to the provided string
    private AudioSource getSound(string soundName) {
        switch (soundName) {
            case "click":
                return sfxSources[0];
            case "anvil":
                return sfxSources[1];
            case "stone":
                return sfxSources[2];
            case "grind":
                return sfxSources[3];
            default:
                Debug.LogError($"Error: Invalid SFX name {soundName}");
                return null;
        }
    }

    // Play the specified sound
    public void playSound(string soundName, bool repeat = false) {
        // Determine which AudioSource to play
        AudioSource src = getSound(soundName);

        // Play the sound
        Debug.Log($"Now Playing sound {soundName}");
        src.Play();
    }

    // Stop the specified sound
    public void stopSound(string soundName) {
        getSound(soundName).Stop();
    }

    // Stop all sounds
    public void stopAllSounds() {
        for (int i = 0; i < sfxSources.Count; i++)
        {
            sfxSources[i].Stop();
        }
    }
    
    // Handle the button click sound
    public static void click() {
        instance.playSound("click");
    }
}
