using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Advertisements;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _junkFoodText;
    [SerializeField] private TextMeshProUGUI _healthyFoodText;
    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private InterstitialAds _ads;
    private LevelTemplates _templates;
    private void Start()
    {
        _templates = GameObject.FindGameObjectWithTag("Levels").GetComponent<LevelTemplates>();
        SetText();
    }
    public void ShowInterstitialAd()
    {
        _ads.ShowAd();
    }
    private void OnEnable()
    {
        SetText();
    }
    private void SetText()
    {
        PlayerData pd = ProcessingPlayerData.GetDataFile();
        _junkFoodText.text = pd.junkfood.ToString();
        _healthyFoodText.text = pd.healthyfood.ToString();
        _coinsText.text = pd.coins.ToString();
        _level.text = pd.levels.ToString();
    }
    private void LoadTradeScene()
    {
        SceneManager.UnloadSceneAsync("SampleScene");
        SceneManager.LoadSceneAsync("TradeFood", LoadSceneMode.Additive);
    }
    public void StartGame()
    {
        _templates.isStarted = true;
    }
    public void TeleportToTradeSceneHealthy()
    {
        Buffer.ChangeFood(true);
        LoadTradeScene();
    }
    public void TeleportToTradeSceneJunk()
    {
        Buffer.ChangeFood(false);
        LoadTradeScene();
    }
}