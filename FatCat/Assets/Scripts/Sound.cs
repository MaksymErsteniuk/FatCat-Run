using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound 
{
    public string name;
    public AudioClip Clip;
    [Range(0f, 1f)] public float Volume;
    public bool Loop;
    public bool PlayOnAwake;
    public bool Mute;
    [HideInInspector] public AudioSource Source;
}