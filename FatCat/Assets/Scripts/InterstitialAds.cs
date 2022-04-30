using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class InterstitialAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{

    [SerializeField] Button _showAdButton;
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    public void LoadAd()
    {
        Debug.Log("Loading Ad: " + _androidAdUnitId);
        Advertisement.Load(_androidAdUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);
        if (adUnitId.Equals(_androidAdUnitId))
        {
            _showAdButton.onClick.AddListener(ShowAd);
        }
    }
    
    public void ShowAd()
    {
        Advertisement.Show(_androidAdUnitId, this);
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_androidAdUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Advertisement.Load(_androidAdUnitId, this);
        }
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    private void OnDestroy()
    {
        // Clean up the button listeners:
        _showAdButton.onClick.RemoveAllListeners();
    }
}