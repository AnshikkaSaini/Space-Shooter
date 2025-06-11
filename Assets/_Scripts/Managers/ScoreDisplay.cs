using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI HighScoreText;


    private void OnEnable()
    {
        int score = PlayerPrefs.GetInt("Score" + SceneManager.GetActiveScene().name, 0);
        scoreText.text = "Score: " + score.ToString();

        int HighScore = PlayerPrefs.GetInt("HighScore" + SceneManager.GetActiveScene().name, 0);
        HighScoreText.text = "HighScore: " + HighScore.ToString();
    }
}
