using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraPositionTrade : MonoBehaviour
{
    private HealthyFoodTradeButtons _healthTradeButtons;
    private JunkFoodTradeButtons _junkTradeButtons;
    private Transform _healthMarket;
    private Transform _junkMarket;
    //private Canvas _canvas;
    private void Start()
    {
        Vector3 cameraPos = this.transform.position;

        if (Buffer.IsHealthyFood)
        {
            //_canvas = GameObject.FindObjectOfType<Canvas>();
            _healthTradeButtons = Resources.FindObjectsOfTypeAll<HealthyFoodTradeButtons>()[0];
            _healthMarket = GameObject.FindObjectOfType<HealthMarket>().gameObject.transform;
            CalculatePosition(_healthMarket, cameraPos);
            //_canvas.gameObject.SetActive(true);
            _healthTradeButtons.gameObject.SetActive(true);
        }
        else
        {
            _junkTradeButtons = Resources.FindObjectsOfTypeAll<JunkFoodTradeButtons>()[0];
            _junkMarket = GameObject.FindObjectOfType<JunkMarket>().gameObject.transform;
            CalculatePosition(_junkMarket, cameraPos);
            _junkTradeButtons.gameObject.SetActive(true);
        }
    }
    private void CalculatePosition(Transform typeOfMarket, Vector3 cameraPosition)
    {
        this.transform.position = new Vector3(cameraPosition.x, cameraPosition.y, typeOfMarket.position.z);
    }
}
