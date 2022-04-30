using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public Sound[] Sounds;
    
    private void Awake()
    {
        SetAudioSource();
    }

    private void Start()
    {
        Play(SoundsName.Theme);
    }

    private void SetAudioSource()
    {
        foreach (Sound sound in Sounds)
        {
            sound.Source = this.gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.Clip;
            sound.Source.playOnAwake = sound.PlayOnAwake;
            sound.Source.loop = sound.Loop;
            sound.Source.volume = sound.Volume;
            sound.Source.mute = sound.Mute;
        }
    }
    
    public void Play(string name)
    {
        Sound sound = System.Array.Find<Sound>(Sounds, s => s.name == name);
        sound.Source.Play();
    }

    public void ChangeVolume(string name, float num)
    {
        Sound sound = System.Array.Find<Sound>(Sounds, s => s.name == name);
        sound.Source.volume = num;
    }
}
