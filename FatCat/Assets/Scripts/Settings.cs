using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    private SoundController _soundController;
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider EffectsSlider;
    private void Start()
    {
        _soundController = GameObject.FindObjectOfType<SoundController>();
        SetSliderValue();
    }
    private void SetSliderValue()
    {
        PlayerData pd = ProcessingPlayerData.GetDataFile();
        MusicSlider.value = pd.volumeOfMusic;
        EffectsSlider.value = pd.volumeOfEffects;
    }
}
