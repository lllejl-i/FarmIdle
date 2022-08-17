using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Music : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public AudioMixerGroup AudioMixer;

    public void ChangeVolume(float volume)
    {

        AudioMixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, volume));

    }



}
