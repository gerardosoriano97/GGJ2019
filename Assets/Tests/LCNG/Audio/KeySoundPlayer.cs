using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class KeySoundPlayer : MonoBehaviour
{
    public AudioSourceExtend audioSource;
    public KeyCode key;
    //public AudioSource audio;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            audioSource.Play();
        }
    }
}
