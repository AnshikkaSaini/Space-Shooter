using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] meteor;

    [SerializeField] private float spawnTime;
    private int i;

    private Camera mainCam;
    private float maxLeft;
    private float maxRight;

    private float timer;
    private float yPos;


    private void Start()
    {
        mainCam = Camera.main;
        StartCoroutine(SetBoundaries());
    }

    // Update is called once per frame
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnTime)
        {
            i = Random.Range(0, meteor.Length);
            var obj = Instantiate(meteor[i], new Vector3(Random.Range(maxLeft, maxRight), yPos, -5f),
                Quaternion.Euler(0, 0, Random.Range(0, 360)));
            var size = Random.Range(0.9f, 1.1f);
            obj.transform.localScale = new Vector3(size, size, 1);

            timer = 0f;
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