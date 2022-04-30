using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class InGameUI : MonoBehaviour
{
    private LevelTemplates _templates;
    private TextMeshProUGUI _healthyFoodUI;
    private TextMeshProUGUI _junkFoodUI;
    private TextMeshProUGUI _amountOfLevels;
    private void Start()
    {
        _healthyFoodUI = GameObject.FindObjectOfType<HealthyFoodUI>().GetComponent<TextMeshProUGUI>();
        _junkFoodUI = GameObject.FindObjectOfType<JunkFoodUI>().GetComponent<TextMeshProUGUI>();
        _amountOfLevels = GameObject.FindObjectOfType<AmountOfLevels>().GetComponent<TextMeshProUGUI>();
        _templates = GameObject.FindGameObjectWithTag("Levels").GetComponent<LevelTemplates>();
        _amountOfLevels.text = ProcessingPlayerData.GetDataFile().levels.ToString();
        ShowStats();
        Food.DeclareDelegate(new Food.UpdateInUIGameStats(ShowStats));
        Patsuyk.SetDelegateUpdateStats(new Patsuyk.UpdateStatsInGameUI(ShowStats));
    }
    private void ShowStats()
    {
        Dictionary<string, int> stats = _templates.GameStats;
        _healthyFoodUI.text = stats["HealthyFood"].ToString();
        _junkFoodUI.text = stats["JunkFood"].ToString();
        // update text in junk and healthy UI, save productivity
        // we can also do this via Update() method
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ReturnToGame()
    {
        Time.timeScale = 1;
    }
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync((int)Scenes.MAIN);
        SceneManager.LoadScene((int)Scenes.MAIN, LoadSceneMode.Additive);
    }
}
