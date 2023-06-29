using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    private static AudioManager instance;

    private AudioManager()
    {}

    public static AudioManager getInstance()
    {
        if (instance == null) instance = new AudioManager();
        return instance;
    }
    
    public void PlayAudio(AudioClip audio)
    {
        var prefab = new GameObject();
        var audioSource = prefab.AddComponent<AudioSource>();
        audioSource.clip = audio;
        audioSource.Play();
        var d = prefab.AddComponent<AudioObject>();
        d.type = AudioObject.AudioType.Sounds;
        d.Destroy();
    }
    
    public void PlayAudio(AudioClip audio, float time)
    {
        var prefab = new GameObject();
        var audioSource = prefab.AddComponent<AudioSource>();
        audioSource.clip = audio;
        audioSource.Play();
        var d = prefab.AddComponent<AudioObject>();
        d.type = AudioObject.AudioType.Sounds;
        d.Init(time);
    }

    public void Vibrate(long time)
    {
        if(PlayerPrefs.GetInt("Vibration") == 1) Vibration.Vibrate(time);
    }

}