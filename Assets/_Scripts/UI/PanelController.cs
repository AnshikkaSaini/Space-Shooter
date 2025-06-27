using UnityEngine;

public class PanelController : MonoBehaviour
{
    [SerializeField] private CanvasGroup cGroup;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    private int alpha;

    private void Start()
    {
        EndGameManager.Instance.RegisterPanelController(this);
    }

    public void ActivateWin()
    {
        cGroup.alpha = 1;
        winScreen.SetActive(true);
    }

    public void ActivateLose()
    {
        cGroup.alpha = 1;
        loseScreen.SetActive(true);
    }
}