using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelTemplates : MonoBehaviour
{
    [HideInInspector] public int AmountFloor;
    [HideInInspector] public int randomSpawn;
    [HideInInspector] public Dictionary<string, int> GameStats;
    [HideInInspector] public string pathToSaveFile;
    [HideInInspector] public bool isStarted;
    public GameObject Patsuyk;
    public Animator animator;
    public GameObject[] platforms;
    public GameObject[] food;
    public Slider MusicSlider;
    public Slider EffectsSlider;
    public GameObject[] Decorations;
    private SoundController _soundController;
    public int maxNumFloor;
    private int minNumFloor;
    private void Awake()
    {
        pathToSaveFile = Application.persistentDataPath + "/player.data";
        if (!File.Exists(pathToSaveFile))
        {
            Debug.Log($"{pathToSaveFile} \nSave SUCCESS");
            ProcessingPlayerData.CreateDataFile(new PlayerData());
        }
        SetCatColor();
        // without this block Buffer scene was spawning every time SampleScene was spawning
        if (!SceneManager.GetSceneByBuildIndex((int)Scenes.BUFFER).isLoaded)
        {
            SceneManager.LoadScene((int)Scenes.BUFFER, LoadSceneMode.Additive);
        }
        isStarted = false;
        // create temporary Dict for storing data about num of junk and healthy food
        GameStats = new Dictionary<string, int> { };
        // add default value in Dict, foreach(similar to "for i in list()" in python)
        foreach(string typeOfFood in new string[] { "HealthyFood", "JunkFood"})
        {
            GameStats.Add(typeOfFood, 0);
        }
        maxNumFloor = 4;
        minNumFloor = 2;
        int numOfLevels = ProcessingPlayerData.GetDataFile().levels;
        if (numOfLevels % 10 == 0)
        {
            int divideNumLevels = numOfLevels / 10;
            maxNumFloor += divideNumLevels;
            minNumFloor += divideNumLevels;
        }
        randomSpawn = Random.Range(minNumFloor, maxNumFloor);
        AmountFloor = randomSpawn;
    }
    private void Start()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex((int)Scenes.MAIN));
        _soundController = GameObject.FindObjectOfType<SoundController>();
        SetVolume();
    }
    private void SetCatColor()
    {
        int currentIndex = ProcessingPlayerData.GetDataFile().currentCatIndex;
        foreach (CatColor catColor in Resources.FindObjectsOfTypeAll<CatColor>())
        {
            if (catColor.Index == currentIndex)
            {
                GameObject.FindObjectOfType<SkinnedMeshRenderer>().material.mainTexture = catColor.CatTexture;
                GameObject.FindObjectOfType<Stomach>().GetComponent<MeshRenderer>().material.mainTexture = catColor.StomachTexture;
            }
        }
    }
    public void SetFloorColor()
    {
        int currentIndex = ProcessingPlayerData.GetDataFile().currentMaterialFloorIndex;
        foreach (FloorColor floorColor in Resources.FindObjectsOfTypeAll<FloorColor>())
        {
            if (floorColor.Index == currentIndex)
            {
                foreach(GameObject gm in GameObject.FindGameObjectsWithTag("Floor"))
                {
                    gm.GetComponent<MeshRenderer>().material.color = floorColor.FloorMaterial.color;
                }
                break;
            }
        }
    }
    private void SetVolume()
    {
        PlayerData pd = ProcessingPlayerData.GetDataFile();
        //MusicSlider.value = pd.volumeOfMusic;
        //EffectsSlider.value = pd.volumeOfEffects;
        _soundController.ChangeVolume(SoundsName.Theme, pd.volumeOfMusic);
        _soundController.ChangeVolume(SoundsName.OnEat, pd.volumeOfEffects);
    }
    // Обновляє randomSpawn, а потім провіряє поле(це як змінна, тільки так правильніше казати) AmountFloor 
    // потім в LevelSpawner воно провіряє чи воно добігло до якогось певного числа(тобто, к-сть floor)
    public void RenewRandomSpawn()
    {
        AmountFloor += randomSpawn;
        randomSpawn = Random.Range(minNumFloor, maxNumFloor);
    }
    public void Update()
    {
        animator.SetBool("IsGameStarted", isStarted);
    }
}
