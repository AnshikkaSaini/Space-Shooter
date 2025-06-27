using UnityEngine;

namespace _Scripts.EnemyScripts
{
    public class PurpleEnemy : Enemy
    {
        [SerializeField] private float speed;
        [SerializeField] private float shootInterval;
        [SerializeField] private Transform leftCanon;
        [SerializeField] private Transform rightCanon;
        [SerializeField] private GameObject bulletPrefab;

        private float shootTimer;

        private void Start()
        {
            rb.velocity = Vector2.down * speed;
        }

        private void Update()
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= shootInterval)
            {
                Instantiate(bulletPrefab, leftCanon.position, Quaternion.identity);
                Instantiate(bulletPrefab, rightCanon.position, Quaternion.identity);
                shootTimer = 0f;
            }
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerStat>().TakeDamage(damage);
                Instantiate(explosionPefab, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }

        public override void HurtSequence()
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Dmg"))
                return;
            anim.SetTrigger("Damage");
        }

        public override void DeathSequence()
        {
            base.DeathSequence();
            Instantiate(explosionPefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}