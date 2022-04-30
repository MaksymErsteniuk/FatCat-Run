using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawn : MonoBehaviour
{
    private LevelTemplates templates;
    private int _numOfFoodForOneSpawn;
    // rotateAngle when corner rotate, correct rotate for food
    public int rotateAngle;
    private float _reduceY = -0.2f;
    private float ztransform;
    private float xtransform;

    private void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Levels").GetComponent<LevelTemplates>();
        ztransform = templates.platforms[0].transform.localScale.z / 2 - 0.5f;
        xtransform = templates.platforms[0].transform.localScale.x / 11;
        Invoke(nameof(Spawn), 0.1f * Time.deltaTime);
    }
    private void Spawn()
    {
        if (Random.Range(1, 6) == 3)
        {
            Instantiate(templates.Patsuyk, this.gameObject.transform.position + new Vector3(0, _reduceY, 0), templates.Patsuyk.transform.rotation);
        }
        _numOfFoodForOneSpawn = 4;
        for (int a = 0; a < _numOfFoodForOneSpawn; a++)
        {
            Vector3 calculatePosition = CalculatePositionForFood(this.gameObject.transform);
            GameObject RandomFood = templates.food[Random.Range(0, templates.food.Length)];
            Vector3 rotateRandFoodInVector = RandomFood.transform.rotation.eulerAngles;
            Vector3 newRotationFood = new Vector3(rotateRandFoodInVector.x, rotateRandFoodInVector.y + rotateAngle, rotateRandFoodInVector.z);
            Quaternion.Euler(newRotationFood);
            Instantiate(RandomFood, calculatePosition, Quaternion.Euler(newRotationFood));
        }
    }
    private Vector3 CalculatePositionForFood(Transform TransformFoodSpawner)
    {
        Vector3 position = TransformFoodSpawner.position;
        float calculateX = Random.Range(xtransform - 3f, xtransform + 1.55f) + position.x;
        float calculateY = position.y;
        float calculateZ = Random.Range(-ztransform, ztransform) + position.z;
        return new Vector3(calculateX, calculateY, calculateZ);
    }
}
