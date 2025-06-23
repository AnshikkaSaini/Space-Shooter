using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    
    [SerializeField] private int maxHealth = 3;
    private float health;
    [SerializeField] private Image healthFill;
    [SerializeField] protected GameObject explosionPefab;
    [SerializeField] public Shield shield;
 
    [SerializeField] private Animator anim;
    private bool canPlayAnim = true;
    void Start()
    {
        health = maxHealth;
       // healthFill.fillAmount = health / maxHealth;
        EndGameManager.Instance.gameOver = false ;
        StartCoroutine(UpdateHealthBar());
    }

    public void TakeDamage(int value)
    {
        if (shield != null && shield.protection && false)
        {
            shield.DamageShield();  // Now this will work
            return;                 // Shield absorbs damage, no health lost
        }

        health -= value;
      
        Debug.Log("Player took damage. Remaining Health: " + health);
        healthFill.fillAmount = health / maxHealth;
        if (canPlayAnim)
        {
            anim.SetTrigger("Damage");
            StartCoroutine(AntiSpamAnimation());
        } ;

      

        if (health <= 0)
        {
            EndGameManager.Instance.gameOver = true;
            EndGameManager.Instance.StartResolveSequence();
            Instantiate(explosionPefab, transform.position, transform.rotation);
            Debug.Log("Player Died.");
            Destroy(gameObject);
        }
    }

    private IEnumerator AntiSpamAnimation()
    {
        canPlayAnim = false;
        yield return new WaitForSeconds(0.15f);
        canPlayAnim = true;
    }
    private IEnumerator UpdateHealthBar()
    {
        float startFill = healthFill.fillAmount;
        float targetFill = health / maxHealth;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 5f; // speed multiplier
            healthFill.fillAmount = Mathf.Lerp(startFill, targetFill, t);
            yield return null;
        }
    }

    public void AddHealth(int healAmount)
    {
        Debug.Log("Healing by: " + healAmount); // 🔍 LOG

        health += healAmount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        StopCoroutine(UpdateHealthBar()); // optional: cancel any running animation
        StartCoroutine(UpdateHealthBar()); // smooth bar update
    }

}