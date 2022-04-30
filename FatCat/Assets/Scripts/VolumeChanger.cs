using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeChanger : MonoBehaviour
{
    private SoundController _soundController;
    private Slider _slider;
    private void Awake()
    {
        _soundController = GameObject.FindObjectOfType<SoundController>();
        _slider = this.GetComponent<Slider>();
    }
    public void ChangeMusicVolume()
    {
        _soundController.ChangeVolume(SoundsName.Theme, _slider.value);
        ProcessingPlayerData.ChangeMusicVolume((float)_slider.value);
    }
    public void ChangeEffectsVolume()
    {
        _soundController.ChangeVolume(SoundsName.OnEat, _slider.value);
        ProcessingPlayerData.ChangeEffectsVolume((float)_slider.value);
    }
}
