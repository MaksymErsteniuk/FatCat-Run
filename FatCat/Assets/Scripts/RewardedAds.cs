using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class RewardedAds : MonoBehaviour, IUnityAdsShowListener, IUnityAdsLoadListener
{
    private const string _androidName = "Rewarded_Android";
    // insert in Action delegate, method from FinishUI - CalculateScore(int num)
    private System.Action<int> _onRewardComplete;
    private static System.Action _setTextInScreenAfterScoreX2;
    public void LoadAd()
    {
        Advertisement.Load(_androidName, this);
    }
    public static void SetActionOnSetText(System.Action setText)
    {
        _setTextInScreenAfterScoreX2 = setText;
    }
    public void SetActionOnReward(System.Action<int> onRewardComplete)
    {
        _onRewardComplete = onRewardComplete;
    }

    public void ShowAd()
    {
        Advertisement.Show(_androidName, this);
    }

    public void OnUnityAdsShowClick(string placementId)
    {

    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log(placementId);
        if (placementId.Equals(_androidName) && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            Debug.Log(_androidName);
            Debug.Log("REWARDED AD");
            _onRewardComplete?.Invoke(2);
            _setTextInScreenAfterScoreX2?.Invoke();
        }
        else
        {
            _onRewardComplete?.Invoke(1);
            _setTextInScreenAfterScoreX2?.Invoke();
        }
    }
    
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
    }

    public void OnUnityAdsShowStart(string placementId)
    {
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Rewarded AD Loaded");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {

    }
}
