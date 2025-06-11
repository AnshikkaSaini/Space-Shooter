using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Fader : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Image loadingBar;
    public static Fader Instance;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float changeValue;
    [SerializeField] private float waitTime;
    [SerializeField] private bool fadeStarted;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // This ensures that if the scene starts with alpha=1, it fades in automatically
        if (canvasGroup.alpha >= 1f)
        {
            StartCoroutine(FadeIn());
        }
    }


    public void FadeLoadInt (int levelIndex)
    {
        StartCoroutine(FadeOutInt(levelIndex));
    }

    public void FaderLoadString(string levelName)
    {
        StartCoroutine(FadeOutString(levelName));
    }

    IEnumerator FadeIn()
    {
        // Important: start fully black
        canvasGroup.alpha = 1f;
        loadingScreen.SetActive(false);

        fadeStarted = true;
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= changeValue;
            yield return new WaitForSeconds(waitTime);
        }

        canvasGroup.alpha = 0f; // Ensure fully transparent at the end
        fadeStarted = false;
    }

    IEnumerator FadeOutString(string levelName)
    {
       if (fadeStarted)
        {
           yield break;
        }
        fadeStarted = true;
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += changeValue;
            yield return new WaitForSeconds(waitTime);
        }

        fadeStarted = false;

     //SceneManager.LoadScene(levelName);
     AsyncOperation ao = SceneManager.LoadSceneAsync(levelName); // ✅ Correct

     ao.allowSceneActivation = false;
     loadingScreen.SetActive(true);
     loadingBar.fillAmount = 0;
     while (ao.isDone == false)
     {
         loadingBar.fillAmount = ao.progress / 0.9f;
         if (ao.progress == 0.9f)
         {
             ao.allowSceneActivation = true;
         }

         yield return null;
     }
     
        StartCoroutine(FadeIn());
    }

    public IEnumerator FadeOutInt(int levelIndex)
    {
        if (fadeStarted)
        {
            yield break;
        }
        fadeStarted = true;
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += changeValue;
            yield return new WaitForSeconds(waitTime);
        }

        AsyncOperation ao = SceneManager.LoadSceneAsync(levelIndex);
        ao.allowSceneActivation = false;
        loadingScreen.SetActive(true);
        loadingBar.fillAmount = 0;
        while (ao.isDone == false)
        {
            loadingBar.fillAmount = ao.progress / 0.9f;
            if (ao.progress == 0.9f)
            {
                ao.allowSceneActivation = true;
            }

            yield return null;
        }

        StartCoroutine(FadeIn());
    }

    
}
