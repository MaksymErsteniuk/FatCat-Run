using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Feed : MonoBehaviour
{
    [SerializeField] private float _frequency = 2f;
    private GameObject playerPrefab;
    public LevelTemplates templates { get; private set; }
    private GameObject Stomach;
    private ParticleSystem _particlesOnEat;
    private SoundController _soundController;
    private float _speedRotation=30f;
    private float _amplitude = 0.5f;
    private void Awake()
    {
        playerPrefab = GameObject.FindGameObjectWithTag("Player");
        Stomach = GameObject.FindObjectOfType<Stomach>().gameObject;
        templates = GameObject.FindWithTag("Levels").GetComponent<LevelTemplates>();
        this.TryGetComponent<ParticleSystem>(out _particlesOnEat);
        _soundController = GameObject.FindObjectOfType<SoundController>();
        if (_particlesOnEat != null) 
        {
            _particlesOnEat.Stop();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // better version
        // if(this.gameObject.TryGetComponent(out smth smth_var))
        if (other.gameObject.name.Contains(playerPrefab.name))
        {
            _soundController.Play(SoundsName.OnEat);

            _particlesOnEat.Play();
            StartCoroutine(WaitForEndOfParticleEffect());
            if (this.tag == "HealthyFood")
            {
                HealthyFood HealthFoodInst = new HealthyFood { FoodName = this.tag, GameStats=templates.GameStats, Stomach=Stomach, byHowMuchManyReduce=0.1f };
                HealthFoodInst.UpdateFood("Healthy Food - ");
                HealthFoodInst.CheckJunkFood(templates);
            }
            else if (this.tag == "JunkFood")
            {
                JunkFood JunkFoodInst = new JunkFood { FoodName = this.tag, GameStats=templates.GameStats, Stomach=Stomach, byHowMuchManyIncrease=0.1f };
                JunkFoodInst.UpdateFood("Junk Food - ");
                JunkFoodInst.CheckJunkFood(templates);
            }
            else
            {

            }
            MeshRenderer mesh = this.GetComponent<MeshRenderer>();
            mesh.enabled = false;
        }
    }
    private IEnumerator WaitForEndOfParticleEffect()
    {
        while (true)
        {
            if (_particlesOnEat.isPlaying == false)
            {
                Destroy(this.gameObject);
                break;
            }
            yield return null;
        }
    }
    private void Update()
    {
        float y = Mathf.Abs(Mathf.Sin(Time.time*_frequency)*_amplitude)+0.6f;
        float x = this.transform.position.x;
        float z = this.transform.position.z;
        this.transform.position = new Vector3(x, y, z);
        Vector3 Rotation = transform.rotation.eulerAngles;
        Rotation.y += _speedRotation*Time.deltaTime;
        transform.rotation = Quaternion.Euler(Rotation);
    }
}
public class Food
{
    public delegate void UpdateInUIGameStats();
    public GameObject Stomach { get; set; }
    public Dictionary<string, int> GameStats { get; set; }
    private static UpdateInUIGameStats _updateStats;
    private string _foodTag;
    // when _numOfPossibleJunk + 1 - game stop
    public int _numOfPossibleJunk = 5;
    public string FoodName
    {
        get { return _foodTag; }
        set { _foodTag = value; }
    }
    public static void DeclareDelegate(UpdateInUIGameStats GameStatsDelegate)
    {
        _updateStats = GameStatsDelegate;
    }
    public void UpdateFood(string name)
    {
        GameStats[_foodTag]++;
        _updateStats();
        Debug.Log(name + GameStats[_foodTag]);
    }
    // update food in json file when level end
    public void AddFoodInFile(string[] tags)
    {
        ProcessingPlayerData.UpdateFile(GameStats[tags[0]], GameStats[tags[1]]);
    }

    public void GetAll()
    {
        PlayerData pd = ProcessingPlayerData.GetDataFile();
        Debug.Log(pd.junkfood);
        Debug.Log(pd.healthyfood);
        Debug.Log(pd.coins);
        Debug.Log(pd.levels);
        Debug.Log(pd.currentCatIndex);
        Debug.Log(pd.currentMaterialFloorIndex);
        Debug.Log(pd.avaibleCat);
        Debug.Log(pd.avaibleMaterialFloor);
    }
    public virtual void ResizeStomach()
    {
    }
    public void CheckJunkFood(LevelTemplates templates)
    {
        if (GameStats["JunkFood"] - GameStats["HealthyFood"] > _numOfPossibleJunk)
        {
            WhenPlayerLose(templates);
        }
        else
        {
            ResizeStomach();
        }
    }
    private void WhenPlayerLose(LevelTemplates templates)
    {
        Resources.FindObjectsOfTypeAll<LoseUI>()[0].gameObject.SetActive(true);
        templates.isStarted = false;
    }
}

public class HealthyFood:Food
{
    private float _minSizeStomach = 0f;
    public float byHowMuchManyReduce;
    public HealthyFood(float num=0.1f)
    {
        byHowMuchManyReduce = num;
    }
    public override void ResizeStomach()
    {
        Vector3 LocalScale = Stomach.transform.localScale;
        //ProgressBar.CurrentValue -= _numOfPossibleJunk;
        if (LocalScale.x > _minSizeStomach)
        {
            Stomach.transform.localScale -= new Vector3(byHowMuchManyReduce, byHowMuchManyReduce, byHowMuchManyReduce);
        }
    }
}

public class JunkFood:Food
{
    public float byHowMuchManyIncrease;
    public JunkFood (float num=0.1f)
    {
        byHowMuchManyIncrease = num;
    }
    public override void ResizeStomach()
    {
        Stomach.transform.localScale += new Vector3(byHowMuchManyIncrease, byHowMuchManyIncrease, byHowMuchManyIncrease);
    }
}