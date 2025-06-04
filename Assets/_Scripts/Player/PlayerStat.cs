using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int value)
    {
        currentHealth -= value;
        Debug.Log("Player took damage. Remaining Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Player Died.");
            Destroy(gameObject);
        }
    }
}