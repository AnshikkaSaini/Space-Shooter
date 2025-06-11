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
       // healthFill.fillAmount = health / maxHealth;
        EndGameManager.endManager.gameOver = false ;
        StartCoroutine(UpdateHealthBar());
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
            EndGameManager.endManager.gameOver = true;
            EndGameManager.endManager.StartResolveSequence();
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

}