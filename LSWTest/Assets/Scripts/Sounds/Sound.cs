using UnityEngine;
using UnityEngine.Audio;
using System;

[Serializable]
public class Sound
{
    public string Id;

    [Range(0f, 1f)] public float Volume = 1;
    [Range(0.1f, 3f)] public float Pitch = 1;
    public bool Loop = false;
 
    public AudioClip Clip;

    [HideInInspector] public AudioSource Source;
}
