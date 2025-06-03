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
        Instantiate(laserBullet, ShootingPoint.position, ShootingPoint.rotation);
    }
}