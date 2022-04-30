using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScreenAfterScoreX2 : MonoBehaviour
{
    private TextMeshProUGUI _healthyFood;
    private TextMeshProUGUI _junkFood;
    
    private void Start()
    {
        _healthyFood = GameObject.FindObjectOfType<HealthyFoodUI>().GetComponent<TextMeshProUGUI>();
        _junkFood = GameObject.FindObjectOfType<JunkFoodUI>().GetComponent<TextMeshProUGUI>();
        RewardedAds.SetActionOnSetText(SetText);
    }
    private void SetText()
    {
        PlayerData pd = ProcessingPlayerData.GetDataFile();
        _healthyFood.text = pd.healthyfood.ToString();
        _junkFood.text = pd.junkfood.ToString();
    }
}
