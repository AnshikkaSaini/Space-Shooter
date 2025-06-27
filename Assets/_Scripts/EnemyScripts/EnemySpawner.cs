using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")] [SerializeField]
    private GameObject[] enemy;

    [Space(15)] [SerializeField] private float enemySpawnTime;

    private float enemyTimer;
    private Camera mainCam;
    private float maxLeft;
    private float maxRight;
    private float yPos;


    private void Start()
    {
        mainCam = Camera.main;
        StartCoroutine(SetBoundaries());
    }

    // Update is called once per frame
    private void Update()
    {
        EnemySpawn();
    }

    private void EnemySpawn()
    {
        enemyTimer += Time.deltaTime;
        if (enemyTimer >= enemySpawnTime)
        {
            var randomPick = Random.Range(0, enemy.Length);
            Instantiate(enemy[randomPick],
                new Vector3(Random.Range(maxLeft, maxRight), yPos, 0)
                , Quaternion.identity);
            enemyTimer = 0;
        }
    }

    private IEnumerator SetBoundaries()
    {
        yield return new WaitForEndOfFrame();
        maxLeft = mainCam.ViewportToWorldPoint(new Vector2(0.15f, 0)).x;
        maxRight = mainCam.ViewportToWorldPoint(new Vector2(0.85f, 0)).x;
        yPos = mainCam.ViewportToWorldPoint(new Vector2(0, 1.1f)).y;
    }
}