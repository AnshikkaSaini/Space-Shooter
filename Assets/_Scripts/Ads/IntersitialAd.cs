using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string _androidAdUnit = "Interstitial_Android";
    [SerializeField] private string _iOSAdUnit = "Interstitial_iOS";
    [SerializeField] private BannerAds bannerAd;
    private string _adUnitID;


    private void Awake(Object instance)
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // prevent duplicates
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
#if UNITY_IOS
        _adUnitID = _iOSAdUnit;
#elif UNITY_ANDROID
        _adUnitID = _androidAdUnit;
#endif
        LoadAd();
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
            Time.timeScale = 1;
            LoadAd();
            bannerAd.LoadBanner();
        }
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.LogError($"Error showing Ad Unit {placementId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("Ad Started");
        Advertisement.Banner.Hide();
        Time.timeScale = 0;
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("Ad Clicked");
    }

    public void LoadAd()
    {
        if (Advertisement.isInitialized) Advertisement.Load(_adUnitID, this);
    }

    public void ShowAd()
    {
        Advertisement.Show(_adUnitID, this);
    }
}