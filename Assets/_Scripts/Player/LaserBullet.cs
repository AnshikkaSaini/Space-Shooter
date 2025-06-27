using UnityEngine;

public class LaserBullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float damage = 1f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.velocity = transform.up * speed;
        Debug.Log("Bullet Direction: " + transform.up + ", Speed: " + speed);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) return; // Ignore self or player collision

        var enemy = other.GetComponent<Enemy>();
        if (enemy != null) enemy.TakeDamage(damage);

        Destroy(gameObject);
    }
}