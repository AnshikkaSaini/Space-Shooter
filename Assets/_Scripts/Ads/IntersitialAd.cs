using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string _androidAdUnit = "Interstitial_Android";
    [SerializeField] private string _iOSAdUnit = "Interstitial_iOS";
    private string _adUnitID;

    void Awake()
    {
#if UNITY_IOS
        _adUnitID = _iOSAdUnit;
#elif UNITY_ANDROID
        _adUnitID = _androidAdUnit;
#endif
        LoadAd();
    }

    public void LoadAd()
    {
        if (Advertisement.isInitialized)
        {
            Advertisement.Load(_adUnitID, this);
        }
    }

    public void ShowAd()
    {
            Advertisement.Show(_adUnitID, this);
    }

    // --- Load Callbacks ---
    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Ad Loaded: " + placementId);
        // Optional: Auto-show ad once loaded
         ShowAd();
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.LogError($"Failed to load Ad Unit {placementId}: {error.ToString()} - {message}");
    }

    // --- Show Callbacks ---
    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId.Equals(_adUnitID) && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            Debug.Log("Ad Completed Successfully");
            // Optionally: Load another ad for next time
            LoadAd();
        }
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.LogError($"Error showing Ad Unit {placementId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("Ad Started");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("Ad Clicked");
    }
}
