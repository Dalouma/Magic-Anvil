using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioMixer mixer;
    public List<AudioSource> bgmusicSources;
    public List<AudioSource> sfxSources;

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
        for (int i = 0; i < bgmusicSources.Count; i++)
        {
            bgmusicSources[i].Stop();
        }
    }

    public enum MusicTrack
    {
        Menu,
        Forge,


    }
    public void ChangeMusic(string trackName)
    {
        StopAllMusic();
        if (trackName == "menu")
        {
            bgmusicSources[0].Play();
        }
        else if (trackName == "forge")
        {
            bgmusicSources[1].Play();
        }
    } 

    public void PlaySFX(string sfxName)
    {
        if ( sfxName == "Anvil")
        {
            sfxSources[0].Play();
        }
        else if ( sfxName == "Victory")
        {
            sfxSources[1].Play();
        }
        else if ( sfxName == "SwordGrind")
        {
            sfxSources[2].Play();
        }
        else if ( sfxName == "Newspaper")
        {
            sfxSources[3].Play();
        }
    }

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
