using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFX : MonoBehaviour
{
    public AudioClip audioClip;
    private AudioSource audioSource;
    private void Start() => audioSource.PlayOneShot(audioClip);
}
