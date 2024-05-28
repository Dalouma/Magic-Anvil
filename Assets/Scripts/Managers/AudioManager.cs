using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioMixer mixer;
    public List<AudioSource> musicSources;
    public List<AudioSource> sfxSources;
    private string currPlaying;

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
    public enum MusicTrack
    {
        Menu,
        Forge,
        Robbery
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

    // SFX handling
    // Get the audio source corresponding to the provided string
    public enum SFXTrack
    {
        Anvil,
        Victory,
        SwordGrind,
        Newspaper,
        Coin,
    }

    private AudioSource GetSound(string soundName) {
        switch (soundName) {
            case "Anvil":
                return sfxSources[0];
            case "Victory":
                return sfxSources[1];
            case "SwordGrind":
                return sfxSources[2];
            case "Newspaper":
                return sfxSources[3];
            case "Coin":
                return sfxSources[4];
            default:
                Debug.LogError($"Error: Invalid SFX name {soundName}");
                return null;
        }
    }

    // Play the specified sound
    public void PlaySFX(string soundName) {
        // Determine which AudioSource to play
        AudioSource src = GetSound(soundName);

        // Play the sound
        Debug.Log($"Now Playing sound {soundName}");
        src.Play();
    }

    // Stop the specified sound
    public void stopSound(string soundName) {
        GetSound(soundName).Stop();
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
        instance.PlaySFX("click");
    }

    // Volume Handling
    public void SetBGVolume(){
        mixer.GetFloat("BGM", out float currVolume);
        if(currVolume == -40){
            mixer.SetFloat("BGM", 0);
        }
        else{
            mixer.SetFloat("BGM", -40);
        }
    }

    public void SetSFXVolume(){
        mixer.GetFloat("BGM", out float currVolume);
        if(currVolume == -40){
            mixer.SetFloat("SFX", 0);
        }
        else{
            mixer.SetFloat("SFX", -40);
        }
    }
}
