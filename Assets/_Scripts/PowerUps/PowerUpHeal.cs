using UnityEngine;

public class PowerUpHeal : MonoBehaviour
{
    [SerializeField] private int healAmount;
    [SerializeField] private AudioClip clickToPlay;

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered by: " + collision.name + ", Tag: " + collision.tag);

        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<PlayerStat>();
            if (player != null)
            {
                Debug.Log("Found PlayerStat, healing...");
                player.AddHealth(healAmount);
                AudioSource.PlayClipAtPoint(clickToPlay, transform.position, 1f);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("PlayerStat NOT found on: " + collision.name);
            }
        }
    }
}