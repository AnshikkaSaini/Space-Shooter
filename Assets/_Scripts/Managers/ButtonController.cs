using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("Fader.fader at ButtonController.Start: " + (Fader.Instance != null));
    }

    public void LoadLevelString(string levelName)
    {
        if (Fader.Instance == null)
        {
            Debug.LogError("Fader not initialized! Make sure it's in the first scene or auto-spawn it.");
            return;
        }

        Debug.Log("Calling FaderLoadString: Fader.fader is " + Fader.Instance);
        Fader.Instance.FaderLoadString(levelName);
    }

    public void LoadLevelInt(int levelIndex)
    {
        Fader.Instance.FadeOutInt(levelIndex);
    }

    public void RestartLevel()
    {
        if (Fader.Instance != null)
        {
            Fader.Instance.FadeLoadInt(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            Debug.LogWarning("Fader.Instance is null! Reloading scene directly.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}