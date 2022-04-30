using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FloorColor : MonoBehaviour
{
    public int Index;
    public Material FloorMaterial;
    private void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(ChangeMaterial);
    }
    private void ChangeMaterial()
    {
        foreach(GameObject Floors in GameObject.FindGameObjectsWithTag("Floor"))
        {
            Floors.GetComponent<MeshRenderer>().material.color = FloorMaterial.color;
        }
        ProcessingPlayerData.ChangeCurrentColorFloorIndex(Index);
    }
}
