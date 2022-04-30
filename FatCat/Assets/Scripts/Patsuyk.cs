using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Patsuyk : MonoBehaviour
{
    private LevelTemplates _templates;
    private int _howManyAddJunkAndHealthyFood = 10;
    private int _speed = 5;
    public delegate void UpdateStatsInGameUI();
    private static UpdateStatsInGameUI _updateStatsInGameUI;
    public static void SetDelegateUpdateStats(UpdateStatsInGameUI updateStats)
    {
        _updateStatsInGameUI = updateStats;
    }
    private void Start()
    {
        _templates = GameObject.FindGameObjectWithTag("Levels").GetComponent<LevelTemplates>();
    }
    private void FixedUpdate()
    {
        transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * _speed);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Quaternion rotate = this.transform.rotation;
        Vector3 euler = rotate.eulerAngles;
        euler.y += 180;
        this.transform.rotation = Quaternion.Euler(euler);
        if (collision.gameObject.tag == "Player")
        {
            string[] foodType = new string[2] { "HealthyFood", "JunkFood" };
            foreach (string typeOfFood in foodType)
            {
                _templates.GameStats[typeOfFood] += _howManyAddJunkAndHealthyFood;
            }
            _updateStatsInGameUI?.Invoke();
            // add 10 helthy and 10 junk food
            Destroy(this.gameObject);
        }
    }
}