using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected int damage;
    [SerializeField] protected GameObject explosionPefab;
    [SerializeField] protected Animator anim;

    [Header("Score")] [SerializeField] protected int scoreValue;

    private void Start()
    {
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        HurtSequence();

        if (health <= 0) DeathSequence();
    }

    public virtual void HurtSequence()
    {
    }

    public virtual void DeathSequence()
    {
        EndGameManager.Instance.UpdateScore(scoreValue);
    }
}