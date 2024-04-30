using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    int _musicStatus;
    int _sfxStatus;

    private void Awake()
    { 
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("Theme");
    }

    private void Update()
    {
        _musicStatus = PlayerPrefs.GetInt("MusicSetting");
        _sfxStatus = PlayerPrefs.GetInt("SoundSetting");

        Debug.Log("music" + _musicStatus);
        Debug.Log("sound" + _sfxStatus);

        if (_musicStatus == 1)
        {
            musicSource.mute = false;
        }
        else
        {
            musicSource.mute = true;
        }

        if (_sfxStatus == 1)
        {
            sfxSource.mute = false;
        }
        else
        {
            sfxSource.mute = true;
        }
    }
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            sfxSource.clip = s.clip;
            sfxSource.Play();
        }
    }
}
