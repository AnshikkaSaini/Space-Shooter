using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpShield : MonoBehaviour
{
    [SerializeField] private AudioClip clickToPlay;
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Shield pickup triggered by: " + collision.name + ", Tag: " + collision.tag);

        if (collision.CompareTag("Player"))
        {
            ShieldActivator shieldActivator = collision.GetComponent<ShieldActivator>();
            if (shieldActivator != null)
            {
                Debug.Log("ShieldActivator found, activating shield...");
                shieldActivator.ActivateShield();
                AudioSource.PlayClipAtPoint(clickToPlay,transform.position,1f);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("ShieldActivator NOT found on: " + collision.name);
            }
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}