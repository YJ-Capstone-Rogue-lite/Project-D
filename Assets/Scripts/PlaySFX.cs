using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFX : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioSource audioSource;
    private void Start() => audioSource = GetComponent<AudioSource>();
    public void Play() => audioSource.PlayOneShot(audioClip);
}
