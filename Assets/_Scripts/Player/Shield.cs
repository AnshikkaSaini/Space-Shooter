using UnityEngine;

public class Shield : MonoBehaviour
{
    public bool protection;
    [SerializeField] private GameObject[] shieldBase;
    private bool allowReset = true;
    private int hitsToDestroy = 3;

    private void OnEnable()
    {
        if (!allowReset) return; // Block unexpected reset

        hitsToDestroy = 3;
        for (var i = 0; i < shieldBase.Length; i++) shieldBase[i].SetActive(true);
        protection = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Shield hit by: " + collision.gameObject.name);

        if (collision.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(1000);
            DamageShield();
        }
        else if (collision.TryGetComponent(out PowerUpShield _))
        {
            Debug.Log("Shield power-up collected!");
            allowReset = true;
            gameObject.SetActive(true); // Triggers OnEnable()
            Destroy(collision.gameObject);
        }
        else if (collision.TryGetComponent(out PowerUpHeal _))
        {
            // Handle healing (if needed)
            Destroy(collision.gameObject);
        }
        else if (collision.TryGetComponent(out PowerUpShooting _))
        {
            // Handle shooting powerup
            Destroy(collision.gameObject);
        }
        else
        {
            Destroy(collision.gameObject);
            DamageShield();
        }
    }

    private void UpdateUI()
    {
        switch (hitsToDestroy)
        {
            case 0:
                for (var i = 0; i < shieldBase.Length; i++) shieldBase[i].SetActive(false);

                break;
            case 1:
                shieldBase[0].SetActive(true);
                shieldBase[1].SetActive(false);
                shieldBase[2].SetActive(false);
                break;
            case 2:
                shieldBase[0].SetActive(true);
                shieldBase[1].SetActive(true);
                shieldBase[2].SetActive(false);
                break;
            case 3:
                shieldBase[0].SetActive(true);
                shieldBase[1].SetActive(true);
                shieldBase[2].SetActive(true);
                break;
            default:
                Debug.Log("wrong");
                break;
        }
    }

    public void DamageShield()
    {
        if (!protection || hitsToDestroy <= 0) return;

        Debug.LogError("11");
        hitsToDestroy--;
        UpdateUI();

        if (hitsToDestroy <= 0)
        {
            hitsToDestroy = 0;
            protection = false;
            allowReset = false; // Prevent auto-reset
            gameObject.SetActive(false);
        }
    }

    public void RepairShield()
    {
        if (!gameObject.activeInHierarchy) return;

        hitsToDestroy = 3;
        protection = true;
        UpdateUI();
    }
}