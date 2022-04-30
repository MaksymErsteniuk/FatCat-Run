using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    private LevelTemplates templates;
    private int floor_panels_number => templates.maxNumFloor + 2;
    private void Start()
    {
        templates = GameObject.FindWithTag("Levels").GetComponent<LevelTemplates>();
        SpawnFloor();
    }
    private void SpawnFloor()
    {
        if (GameObject.FindGameObjectsWithTag("Floor").Length >= floor_panels_number)
        {
            GameObject finish = Instantiate(templates.platforms[3], this.transform.position, templates.platforms[3].transform.rotation);
            templates.SetFloorColor();
            Destroy(this.gameObject);
            //CancelInvoke();
        }
        /* тут else if, інакше буде визвано 2 if цей шо вище і нижче, бо зверху
        там менше і того воно буде всерівно робитися, бо воно рахується як і не більше
        */
        else if (GameObject.FindGameObjectsWithTag("Floor").Length % 2 == 0)
        {
            GameObject randomCorner = templates.platforms[Random.Range(1, 3)];
            GameObject floorRotate = Instantiate(randomCorner, this.transform.position, randomCorner.transform.rotation);
            templates.SetFloorColor();
            Destroy(this.gameObject);
            templates.RenewRandomSpawn();
        }
        else if (GameObject.FindGameObjectsWithTag("Floor").Length < templates.AmountFloor)
        {
            GameObject floor = Instantiate(templates.platforms[0], this.transform.position, templates.platforms[0].transform.rotation);
            templates.SetFloorColor();
            Destroy(this.gameObject);
            templates.RenewRandomSpawn();
        }
    }
}
