using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    // static GameObject container;

    // ---싱글톤으로 선언--- //
    static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (!instance)
            {
                // container = new GameObject();
                // container.name = "SoundManager";
                // instance = container.AddComponent(typeof(SoundManager)) as SoundManager;
                Instantiate(Resources.Load<GameObject>("SoundManager"));
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
        private set => instance = value;
    }
    
    public AudioMixer audioMixer;
    public AudioSource[] musicsource;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(instance);
    }

    public static void SetVolume(float volume) => Instance.audioMixer.SetFloat("MAster", Mathf.Log10(volume) * 20);

    public static void SetMusicVolume(float volume) => Instance.audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    public static void SetSFXVolume(float volume) => Instance.audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    public static void SetAllVolumes(float volume)
    {
        Instance.audioMixer.SetFloat("BGM", volume);
        Instance.audioMixer.SetFloat("SFX", volume);
    }

    public static void SetMusic(AudioClip audioClip)
    {
        Instance.musicsource[0].clip = audioClip;
        instance.musicsource[0].Play();
    }
    public static void PlaySFX(AudioClip audioClip) => Instance.musicsource[1].PlayOneShot(audioClip);
}
