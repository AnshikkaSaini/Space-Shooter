using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private string _androidGameId;
    [SerializeField] private string _iOSGameId;
    [SerializeField] private bool _testMode = true;

    private string _gameId;

    private void Awake()
    {
        if (!Advertisement.isInitialized)
        {
            _gameId = Application.platform == RuntimePlatform.IPhonePlayer
                ? _iOSGameId
                : _androidGameId;

            Advertisement.Initialize(_gameId, _testMode, this);
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogError($"Unity Ads Initialization Failed: {error} - {message}");
    }
}