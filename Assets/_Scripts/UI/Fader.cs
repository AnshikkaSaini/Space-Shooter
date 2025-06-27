using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    public static Fader Instance;

    [Header("Fade Settings")] [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField] private float changeValue = 0.05f;
    [SerializeField] private float waitTime = 0.01f;

    [Header("Loading Screen")] [SerializeField]
    private GameObject loadingScreen;

    [SerializeField] private Image loadingBar;

    private bool fadeStarted;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (canvasGroup.alpha >= 0.99f) StartCoroutine(FadeIn());
    }

    public void FadeLoadInt(int levelIndex)
    {
        StartCoroutine(FadeOutInt(levelIndex));
    }

    public void FaderLoadString(string levelName)
    {
        StartCoroutine(FadeOutString(levelName));
    }

    private IEnumerator FadeIn()
    {
        loadingScreen.SetActive(false);
        fadeStarted = true;

        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= changeValue;
            yield return new WaitForSeconds(waitTime);
        }

        canvasGroup.alpha = 0f;
        fadeStarted = false;
    }

    private IEnumerator FadeOutString(string levelName)
    {
        if (fadeStarted) yield break;
        fadeStarted = true;

        // Fade to black
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += changeValue;
            yield return new WaitForSeconds(waitTime);
        }

        canvasGroup.alpha = 1f;
        yield return new WaitForSeconds(0.3f); // Optional pause at black screen

        // Begin async load
        var ao = SceneManager.LoadSceneAsync(levelName);
        ao.allowSceneActivation = false;

        loadingScreen.SetActive(true);
        loadingBar.fillAmount = 0;

        while (!ao.isDone)
        {
            loadingBar.fillAmount = Mathf.Clamp01(ao.progress / 0.9f);
            if (ao.progress >= 0.9f) ao.allowSceneActivation = true;
            yield return null;
        }

        yield return StartCoroutine(FadeIn());
    }

    public IEnumerator FadeOutInt(int levelIndex)
    {
        if (fadeStarted) yield break;
        fadeStarted = true;

        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += changeValue;
            yield return new WaitForSeconds(waitTime);
        }

        canvasGroup.alpha = 1f;
        yield return new WaitForSeconds(0.3f);

        var ao = SceneManager.LoadSceneAsync(levelIndex);
        ao.allowSceneActivation = false;

        loadingScreen.SetActive(true);
        loadingBar.fillAmount = 0;

        while (!ao.isDone)
        {
            loadingBar.fillAmount = Mathf.Clamp01(ao.progress / 0.9f);
            if (ao.progress >= 0.9f) ao.allowSceneActivation = true;
            yield return null;
        }

        yield return StartCoroutine(FadeIn());
    }
}