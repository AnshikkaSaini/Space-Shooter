using TMPro;
using UnityEngine;

public class ScoreRegistration : MonoBehaviour
{
    private void Start()
    {
        var textForRegistration = GetComponent<TextMeshProUGUI>();
        EndGameManager.Instance.RegisterScoreText(textForRegistration);
        textForRegistration.text = "Score: 0";
    }
}