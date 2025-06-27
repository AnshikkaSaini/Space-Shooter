using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI HighScoreText;


    private void OnEnable()
    {
        var score = PlayerPrefs.GetInt("Score" + SceneManager.GetActiveScene().name, 0);
        scoreText.text = "Score: " + score;

        var HighScore = PlayerPrefs.GetInt("HighScore" + SceneManager.GetActiveScene().name, 0);
        HighScoreText.text = "HighScore: " + HighScore;
    }
}