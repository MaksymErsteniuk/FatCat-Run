using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private GameObject FinishGUI;
    private GameObject _inGameGUI;
    private LevelTemplates templates;
    private void Start()
    {
        _inGameGUI = Resources.FindObjectsOfTypeAll<InGameUI>()[0].gameObject;
        FinishGUI = Resources.FindObjectsOfTypeAll<FinishUI>()[0].gameObject;
        templates = GameObject.FindGameObjectWithTag("Levels").GetComponent<LevelTemplates>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _inGameGUI.SetActive(false);
            FinishGUI.SetActive(true);
            templates.isStarted = false;
            Food FoodInst = new Food { GameStats=templates.GameStats };
            //FoodInst.AddFoodInFile(new string[] { "JunkFood", "HealthyFood" });
            FoodInst.GetAll();
        }
    }
}
