using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject laserBullet;
    [SerializeField] private float shootingInterval = 0.5f;

    [Header("Basic Attack")] [SerializeField]
    private Transform ShootingPoint;

    [Header("Upgrade Points")] [SerializeField]
    private Transform leftCanon;

    [SerializeField] private Transform rightCanon;
    [SerializeField] private AudioSource source;

    private float intervalReset;

    private int upgradeLevel;

    private void Start()
    {
        intervalReset = shootingInterval;
    }

    private void Update()
    {
        shootingInterval -= Time.deltaTime;
        if (shootingInterval <= 0f)
        {
            Shooting();
            shootingInterval = intervalReset;
        }
    }

    public void IncreaseUpgrade(int increaseAmount)
    {
        upgradeLevel += increaseAmount;
        if (upgradeLevel > 4) upgradeLevel = 4;
    } // ReSharper disable Unity.PerformanceAnalysis
    private void Shooting()
    {
        if (source != null) source.Play();

        switch (upgradeLevel)
        {
            case 0:
                Instantiate(laserBullet, ShootingPoint.position, Quaternion.identity);
                break;
            case 1:
                Instantiate(laserBullet, leftCanon.position, Quaternion.identity);
                Instantiate(laserBullet, rightCanon.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(laserBullet, ShootingPoint.position, Quaternion.identity);
                Instantiate(laserBullet, leftCanon.position, Quaternion.identity);
                Instantiate(laserBullet, rightCanon.position, Quaternion.identity);
                break;
        }
    }
}