using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDecoration : MonoBehaviour
{
    private const int _maxRange = 100;
    private const int _minRange = 0;
    private const int _chance = 35;
    private GameObject _floor;
    private GameObject[] _decorations;
    private LevelTemplates _templates;
    [SerializeField] private Vector3 _rotateTo;
    private void Awake()
    {
        _floor = GameObject.FindGameObjectWithTag("Floor");
        _templates = GameObject.FindGameObjectWithTag("Levels").GetComponent<LevelTemplates>();
        _decorations = _templates.Decorations;
    }
    private void Start()
    {
        Spawn();
    }
    private void Spawn()
    {
        if (Random.Range(_maxRange, _minRange + 1) <= _chance)
        {
            GameObject decoration = _decorations[Random.Range(0, _decorations.Length)];
            Vector3 rotate = decoration.transform.rotation.eulerAngles + _rotateTo;
            Vector3 lengthOfDecoration = decoration.GetComponent<BoxCollider>().size;
            Vector3 spawnPosition = this.transform.position;
            Vector3 lengthOfFloor = _floor.GetComponent<BoxCollider>().size;
            // when divided for 2 get original size, when multiply on scale we get height
            float heightOfFloor = lengthOfFloor.y / 2 * _floor.transform.localScale.y / 2;
            // we devide for 4, because when we divide for 2 get normal size of object, then again divide for we get middle of object
            // sqrt() because of formula for calculation length of segment
            // a = sqrt((x1-x)^2 + (y1-y)^2 + (z1-z)^2); a - length
            // in our case, x1=x, z1=z, y = 0(spawn point coords of y equal 0 ) => y1 = a^2
            Vector3 decorationPosition = new Vector3(spawnPosition.x, Mathf.Sqrt(lengthOfDecoration.y / 4) - heightOfFloor, spawnPosition.z);
            Instantiate(decoration, decorationPosition, Quaternion.Euler(rotate));
        }
    }
}
