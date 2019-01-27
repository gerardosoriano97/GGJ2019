using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class KeySoundPlayer : MonoBehaviour
{
    public AudioSourceExtend audioSource;
    public AudioSource sssource;
    public KeyCode key;
    public UnityEvent KeyPress;
    //public AudioSource audio;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            if (sssource == null)
                audioSource.Play();
            else
                sssource.Play();
        }
    }
}
