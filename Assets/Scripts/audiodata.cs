using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audiodata : MonoBehaviour
{
    public AudioForObject[] Audios;
    public static audiodata instance;
    public GameObject emptysource;

    private AudioSource[] sources;
    public void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAllSources();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void InitializeAllSources()
    {
        for (int z = 0; z < Audios.Length; z++)
        {
            sources = new AudioSource[Audios[z].audio.maxAllowedSources];
            for (int k = 0; k < Audios[z].audio.maxAllowedSources; k++)
            {
                sources[k] = Instantiate(emptysource).GetComponent<AudioSource>();
                sources[k].clip = Audios[z].audio.clip;
                sources[k].volume = Audios[z].audio.volume * (PlayerPrefs.GetInt("Audio", 100) / 100f);
                sources[k].gameObject.transform.parent = transform;
            }

            Audios[z].audio.sources = sources;
            Audios[z].audio.currentSource = 0;
            
        }
    }
    public void playAudio(string name)
    {
        for (int z = 0; z < Audios.Length; z++)
        {
            if (Audios[z].index != name) continue;
            Audios[z].audio.currentSource++;
            if (Audios[z].audio.currentSource >= Audios[z].audio.maxAllowedSources)
                Audios[z].audio.currentSource = 0;
            if (Audios[z].audio.sources[Audios[z].audio.currentSource].isPlaying == false)
                Audios[z].audio.sources[Audios[z].audio.currentSource].Play();
        }
    }

    public void ReloadVolume()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        InitializeAllSources();
    }
}
[System.Serializable]
public struct audioSet
{
    public AudioClip clip;
    public float volume;
    public int maxAllowedSources;
    [HideInInspector]
    public AudioSource[] sources;
    [HideInInspector]
    public int currentSource;
}
[System.Serializable]
public struct AudioForObject
{
    public string index;
    public audioSet audio;
}