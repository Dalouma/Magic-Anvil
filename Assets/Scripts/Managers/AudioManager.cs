using System;
using System.Collections;
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
}
