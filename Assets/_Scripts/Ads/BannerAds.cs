using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAds : MonoBehaviour
{
    [SerializeField] private string _androidAdUnit = "Banner_Android";
    [SerializeField] private string _iOSAdUnit = "Banner_iOS";
    private string _adUnitID = null;

    [SerializeField] private BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;

    private void Start()
    {
#if UNITY_IOS
        _adUnitID = _iOSAdUnit;
#elif UNITY_ANDROID
        _adUnitID = _androidAdUnit;
#endif

        LoadBanner();
    }

    public void LoadBanner()
    {
        if (Advertisement.isInitialized)
        {
            Advertisement.Banner.SetPosition(_bannerPosition);

            BannerLoadOptions options = new BannerLoadOptions()
            {
                loadCallback = OnBannerLoaded,
                errorCallback = OnBannerError
            };

            Advertisement.Banner.Load(_adUnitID, options);
        }

        
    }

    private void OnBannerLoaded()
    {
        Debug.Log("Banner loaded successfully.");
        Advertisement.Banner.Show(_adUnitID);
    }

    private void OnBannerError(string message)
    {
        Debug.LogError($"Banner failed to load: {message}");
    }
}