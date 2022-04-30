using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class JunkFoodTradeButtons : MonoBehaviour
{
    private TextMeshProUGUI _textOfNotificationBuyingCoins;
    private CanvasGroup _canvasGroupOfNotificationOfBuyingCoins;
    private NotificationOfBuyingCoins _notification;
    private string _stringToDisplayNotification = "+ ";
    private float _speedOfCoroutine = 0.1f;
    private float _speedOfTransparentEffect = 0.08f;
    private float _speedOfLifting = -1f;
    private Vector2 _firstPositionOfNotification;

    private void Awake()
    {
        ProcessingPlayerData.SetDelegateShowMessage(new ProcessingPlayerData.ShowMessage(Show));
        _notification = GameObject.FindObjectOfType<NotificationOfBuyingCoins>();
        _textOfNotificationBuyingCoins = _notification.GetComponent<TextMeshProUGUI>();
        _canvasGroupOfNotificationOfBuyingCoins = _notification.GetComponent<CanvasGroup>();
        _firstPositionOfNotification = _notification.transform.position;
    }
    public void Trade(string priceInFoodAndPriceInCoins)
    {
        ResetNotification();
        string[] splitPrices = priceInFoodAndPriceInCoins.Split(' ');
        Debug.Log(splitPrices[0]);

        ProcessingPlayerData.Trade(Convert.ToInt32(splitPrices[0]), "junkfood", out int? price, Convert.ToInt32(splitPrices[1]));

        if (price == null)
        {
            return;
        }
        _textOfNotificationBuyingCoins.text = _stringToDisplayNotification + price.ToString() + " coins";
        StartCoroutine(DoTransparentEffect());
    }
    private void ResetNotification()
    {
        // del previous text
        _notification.transform.position = _firstPositionOfNotification;
        _textOfNotificationBuyingCoins.text = "";
        _canvasGroupOfNotificationOfBuyingCoins.alpha = 1;

    }
    private IEnumerator DoTransparentEffect()
    {
        while (true)
        {
            if (_canvasGroupOfNotificationOfBuyingCoins.alpha <= 0)
            {
                break;
            }
            SetNewPosition();
            SetNewAlpha();
            yield return new WaitForSeconds(_speedOfCoroutine);
        }
    }
    private void SetNewPosition()
    {
        Vector2 positionOfNotification = _notification.transform.position;
        positionOfNotification.y -= _speedOfLifting;
        _notification.transform.position = positionOfNotification;
    }
    private void SetNewAlpha()
    {
        _canvasGroupOfNotificationOfBuyingCoins.alpha -= _speedOfTransparentEffect;
    }
    private void Show(string message)
    {
        Resources.FindObjectsOfTypeAll<YouDoNotHaveEnoughMoneyMessage>()[0].gameObject.SetActive(true);
    }
}
