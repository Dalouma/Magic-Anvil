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
    private string currMusicPlaying;

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
        Robbery,
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
        if(!restart && trackName == currMusicPlaying) {
            Debug.Log("Already Playing");
            return;
        }

        // Track is different or restart was specified. Stop music and start a new track
        StopAllMusic();
        currMusicPlaying = trackName;
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
        Stone,
        Click,
    }

    private AudioSource GetSound(string soundName) {
        // Lambda function for if the switch misses
        AudioSource OnFail() {
            Debug.LogError($"Error: Invalid SFX name {soundName}");
            return null;
        }

        // Return the appropriate AudioSource based on the name
        return soundName switch
        {
            "Anvil" =>      sfxSources[0],
            "Victory" =>    sfxSources[1],
            "SwordGrind" => sfxSources[2],
            "Newspaper" =>  sfxSources[3],
            "Coin" =>       sfxSources[4],
            "Stone" =>      sfxSources[5],
            "Click" =>      sfxSources[6],
            "Gem" =>        sfxSources[7],
            "Attack" =>     sfxSources[UnityEngine.Random.Range(8, 11)],
            "Defeat" =>     sfxSources[12],
            _ => OnFail(),
        };
    }

    // Play the specified sound
    public void PlaySFX(string soundName, bool allowOverlap = true) {
        // Determine which AudioSource to play
        AudioSource src = GetSound(soundName);

        // Play the sound
        // AudioSources can only "play" one sound at a time, so if we want to overlap a different method is needed
        if(!src.isPlaying) {
            // Audio source is not playing. Play it
            Debug.Log($"Now Playing sound {soundName}");
            src.Play();
        } else if (allowOverlap) {
            // Audio source is playing but overlap is allowed
            Debug.Log($"Now Playing sound {soundName} (OneShot)");
            src.PlayOneShot(src.clip);
        }
    }

    // Stop the specified sound
    public void StopSFX(string soundName) {
        GetSound(soundName).Stop();
    }

    // Stop all sounds
    public void StopAllSFX() {
        for (int i = 0; i < sfxSources.Count; i++)
        {
            sfxSources[i].Stop();
        }
    }
    
    // Handle the button click sound
    public static void Click() {
        instance.PlaySFX("Click");
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
        mixer.GetFloat("SFX", out float currVolume);
        if(currVolume == -40){
            mixer.SetFloat("SFX", 0);
        }
        else{
            mixer.SetFloat("SFX", -40);
        }
    }
}
