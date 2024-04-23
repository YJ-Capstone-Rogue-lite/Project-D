using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    
    public AudioSource[] musicsource;


    public void setMusicVolume(float volume)
    {
        for(int i =0; i < musicsource.Length; i++)
        {
            musicsource[i].volume = volume;
        }
    }

  
}
