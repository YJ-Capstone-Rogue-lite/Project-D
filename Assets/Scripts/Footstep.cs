using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour
{
    public AudioClip footstep;

    public float startTime;
    public float trimmedTime;
    public int footstepCount;
    private Queue<AudioClip> queue = new Queue<AudioClip>();
    private AudioSource audioSource;

    private void Awake()
    {
        var time = startTime;
        for(int i = 0; i < footstepCount; i++)
        {
            var temp = TrimSilence(footstep, time, time += trimmedTime);
            queue.Enqueue(temp);
        }
        audioSource = GetComponent<AudioSource>();
    }

    public void Next()
    {
        var temp = queue.Dequeue();
        audioSource.PlayOneShot(temp);
        queue.Enqueue(temp);
    }
    int idx = 0;
    private AudioClip TrimSilence(AudioClip clip, float startTime, float endTime)
    {
        float[] samples = new float[clip.samples * clip.channels];
        clip.GetData(samples, 0);

        int startIndex = (int)(startTime * clip.frequency) * clip.channels;
        int endIndex = (int)(endTime * clip.frequency) * clip.channels;

        float[] trimmedSamples = new float[endIndex - startIndex + 1];
        for(int i = startIndex; i < endIndex; i++) trimmedSamples[i-startIndex] = samples[i];
        AudioClip trimmedClip = AudioClip.Create("trimmed" + idx++, trimmedSamples.Length/clip.channels, clip.channels, clip.frequency, false);
        trimmedClip.SetData(trimmedSamples, 0);
        return trimmedClip;
    }
}
