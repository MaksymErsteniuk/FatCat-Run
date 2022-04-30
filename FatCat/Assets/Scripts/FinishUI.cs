using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class FinishUI : MonoBehaviour
{
    [SerializeField] private RewardedAds _rewardedAd;
    private LevelTemplates _templates;
    private TextMeshProUGUI _healthyFood;
    private TextMeshProUGUI _junkFood;
    private void OnEnable()
    {
        _templates = GameObject.FindGameObjectWithTag("Levels").GetComponent<LevelTemplates>();
        _healthyFood = GameObject.FindObjectOfType<HealthyFoodUI>().GetComponent<TextMeshProUGUI>();
        _junkFood = GameObject.FindObjectOfType<JunkFoodUI>().GetComponent<TextMeshProUGUI>();
        _healthyFood.text = _templates.GameStats["HealthyFood"].ToString();
        _junkFood.text = _templates.GameStats["JunkFood"].ToString();
        _rewardedAd.LoadAd();
    }

    public void Score2X()
    {
        _rewardedAd.SetActionOnReward(CalculateScore);
        _rewardedAd.ShowAd();
    }
    public void WithoutAdditionScore()
    {
        CalculateScore(1);
    }
    private void CalculateScore(int HowManyToMultiply)
    {
        string[] foodType = new string[2] {"JunkFood", "HealthyFood"};
        foreach (string typeOfFood in foodType)
        {
            _templates.GameStats[typeOfFood] *= HowManyToMultiply;
        }
        ProcessingPlayerData.UpdateFile(_templates.GameStats[foodType[0]], _templates.GameStats[foodType[1]]);
    }
    public void ResetScene()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex((int)Scenes.MAIN));
        SceneManager.LoadScene((int)Scenes.MAIN, LoadSceneMode.Additive);
    }
}