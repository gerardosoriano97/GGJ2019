using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceMultiple : AudioSourceExtend
{
    public List<AudioSource> audioSources;
    public override void Play()
    {
        foreach (var audioSource in audioSources)
        {
            audioSource.Play();
        }
    }
}
