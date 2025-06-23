using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    public static EndGameManager Instance;
    public bool gameOver;
    private PanelController panelController;
    private TextMeshProUGUI scoreTextComponent;
    private int score;
    [HideInInspector]
    public string lvlUnlock = "Level Unlock";

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

    public void UpdateScore(int addScore)
    {
        score += addScore;
        scoreTextComponent.text = "Score:" + score.ToString();
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
        {
            WinGame();
        }
        else
        {
            LoseGame();
        }
    }

    public void WinGame()
    {
        ScoreSet();
        panelController.ActivateWin();
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLevel > PlayerPrefs.GetInt(lvlUnlock,0))
        {
            PlayerPrefs.SetInt(lvlUnlock,nextLevel);
        }
    }
    
    public void LoseGame()
    {
        ScoreSet();
        panelController.ActivateLose();
    }

    private void ScoreSet()
    {
        PlayerPrefs.SetInt("Score" + SceneManager.GetActiveScene().name, score);
        int highScore = PlayerPrefs.GetInt("HighScore" + SceneManager.GetActiveScene().name, 0);
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore" + SceneManager.GetActiveScene().name,score);
        }

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
}
