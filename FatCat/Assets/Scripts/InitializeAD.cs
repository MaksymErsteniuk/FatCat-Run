using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InitializeAD : MonoBehaviour, IUnityAdsInitializationListener
{
    private const string _androidAdsId = "4500603";
    private bool _isTestMode = true;
    private void Awake()
    {
        Advertisement.Initialize(_androidAdsId, _isTestMode, this);
    }

    public void OnInitializationComplete()
    {
    }
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
    }
}
