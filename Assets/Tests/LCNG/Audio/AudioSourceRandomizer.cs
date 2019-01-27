using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceRandomizer : AudioSourceExtend
{
    public List<AudioSource> audioSources;
    public override void Play()
    {
        foreach (var audioSource in audioSources)
        {
            audioSource.Stop();
        }
        if (audioSources.Count > 0)
            audioSources[Random.Range(0, audioSources.Count)].Play();
    }
}
