using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject laserBullet;
    [SerializeField] private Transform ShootingPoint;
    [SerializeField] private float shootingInterval = 0.5f;
    private float intervalReset;

    void Start()
    {
        intervalReset = shootingInterval;
    }

    void Update()
    {
        shootingInterval -= Time.deltaTime;
        if (shootingInterval <= 0f)
        {
            Shooting();
            shootingInterval = intervalReset;
        }
    }

    private void Shooting()
    {
        Vector3 spawnPos = ShootingPoint.position;
        spawnPos.z = 0f; // For 2D games, ensure it's on the visible Z plane
        Instantiate(laserBullet, spawnPos, ShootingPoint.rotation);
    }
}