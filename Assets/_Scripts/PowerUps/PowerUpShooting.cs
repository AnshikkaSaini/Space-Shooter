using System;
using UnityEngine;

public class PowerUpShooting : MonoBehaviour
{
    [SerializeField] private AudioClip clickToPlay;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Shooting power-up triggered by: " + collision.name + ", Tag: " + collision.tag);

        if (collision.CompareTag("Player"))
        {
            PlayerShooting player = collision.GetComponent<PlayerShooting>();
            if (player == null)
            {
                // Try finding in parent if it's not directly on the collider object
                player = collision.GetComponentInParent<PlayerShooting>();
            }

            if (player != null)
            {
                Debug.Log("Found PlayerShooting, applying upgrade...");
                player.IncreaseUpgrade(1);
                AudioSource.PlayClipAtPoint(clickToPlay,transform.position,1f);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("PlayerShooting component NOT found on: " + collision.name);
            }
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}