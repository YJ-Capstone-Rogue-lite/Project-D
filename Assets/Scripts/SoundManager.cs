using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicsource;

    public void setMusicVolume(float volume)
    {
        musicsource.volume = volume;
    }
}
