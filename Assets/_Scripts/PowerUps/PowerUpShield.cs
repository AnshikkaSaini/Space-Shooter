using UnityEngine;

public class PowerUpShield : MonoBehaviour
{
    [SerializeField] private AudioClip clickToPlay;

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Shield pickup triggered by: " + collision.name + ", Tag: " + collision.tag);

        if (collision.CompareTag("Player"))
        {
            var shieldActivator = collision.GetComponent<ShieldActivator>();
            if (shieldActivator != null)
            {
                Debug.Log("ShieldActivator found, activating shield...");
                shieldActivator.ActivateShield();
                AudioSource.PlayClipAtPoint(clickToPlay, transform.position, 1f);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("ShieldActivator NOT found on: " + collision.name);
            }
        }
    }
}