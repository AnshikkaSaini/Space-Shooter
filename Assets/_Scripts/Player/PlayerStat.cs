using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    
    [SerializeField] private int maxHealth = 3;
    private float health;
    [SerializeField] private Image healthFill;
    [SerializeField] protected GameObject explosionPefab;
 
    [SerializeField] private Animator anim;
    private bool canPlayAnim = true;
    void Start()
    {
        health = maxHealth;
        healthFill.fillAmount = health / maxHealth;
    }

    public void TakeDamage(int value)
    {
       
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

}