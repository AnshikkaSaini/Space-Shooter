using UnityEngine;

public class PurpleBullet : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * speed; // Multiply by speed for actual motion
    }

    private void OnTriggerEnter2D(Collider2D collision) // ⛳ Use OnTriggerEnter2D for 2D colliders
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerStat>()?.TakeDamage((int)damage); // Call damage if applicable
            Destroy(gameObject);
        }
    }
}