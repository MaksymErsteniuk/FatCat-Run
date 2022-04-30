using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseUI : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex((int)Scenes.MAIN));
        SceneManager.LoadScene((int)Scenes.MAIN, LoadSceneMode.Additive);
    }
}
