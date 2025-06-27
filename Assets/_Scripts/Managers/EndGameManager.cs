using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    public static EndGameManager Instance;
    public bool gameOver;

    [HideInInspector] public string lvlUnlock = "Level Unlock";

    private PanelController panelController;
    private int score;
    private TextMeshProUGUI scoreTextComponent;

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void UpdateScore(int addScore)
    {
        score += addScore;
        scoreTextComponent.text = "Score:" + score;
    }


    public void StartResolveSequence()
    {
        StopCoroutine(nameof(ResolveSequence));
        StartCoroutine(ResolveSequence());
    }

    private IEnumerator ResolveSequence()
    {
        yield return new WaitForSeconds(2);
        ResolveGame();
    }

    public void ResolveGame()
    {
        if (gameOver == false)
            WinGame();
        else
            LoseGame();
    }

    public void WinGame()
    {
        ScoreSet();
        panelController.ActivateWin();
        var nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLevel > PlayerPrefs.GetInt(lvlUnlock, 0)) PlayerPrefs.SetInt(lvlUnlock, nextLevel);
    }

    public void LoseGame()
    {
        ScoreSet();
        panelController.ActivateLose();
    }

    private void ScoreSet()
    {
        PlayerPrefs.SetInt("Score" + SceneManager.GetActiveScene().name, score);
        var highScore = PlayerPrefs.GetInt("HighScore" + SceneManager.GetActiveScene().name, 0);
        if (score > highScore) PlayerPrefs.SetInt("HighScore" + SceneManager.GetActiveScene().name, score);

        score = 0;
    }


    public void RegisterPanelController(PanelController pc)
    {
        panelController = pc;
    }

    public void RegisterScoreText(TextMeshProUGUI scoreText)
    {
        scoreTextComponent = scoreText;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        gameOver = false; // ✅ Reset game state
        score = 0; // ✅ Optional: reset score if per-level
        scoreTextComponent = null; // ✅ Ensure new scene can re-register it
        panelController = null; // ✅ Ensure new scene can re-register it
    }
}