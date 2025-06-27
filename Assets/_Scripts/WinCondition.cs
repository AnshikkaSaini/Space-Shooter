using UnityEngine;

public class WinCondition : MonoBehaviour
{
    [SerializeField] private float possibleWinTime;
    [SerializeField] private GameObject[] spawner;
    private float timer;


    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (EndGameManager.Instance.gameOver) return;

        timer += Time.deltaTime;
        if (timer >= possibleWinTime)
        {
            for (var i = 0; i < spawner.Length; i++) spawner[i].SetActive(false);
            EndGameManager.Instance.StartResolveSequence();
            gameObject.SetActive(false);
        }
    }
}