using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


public class Meteor : Enemy ,IDamageable
{
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float rotateSpeed;
     private float speed; 
    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
        rb.velocity = Vector2.down * speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,rotateSpeed * Time.deltaTime);
    }

    public override void HurtSequence()
    {
        
    }

    public override void DeathSequence()
    {
        base.DeathSequence();
        Instantiate(explosionPefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            //Destroy(otherCollider.gameObject);
            PlayerStat player = otherCollider.GetComponent<PlayerStat>();
            player.TakeDamage(damage);
            Instantiate(explosionPefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public void TakeDamage (int value)
    {
        Health -= value;

        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public int Health { get; set; } = 1;

    private void OnBecameInvisible()
    {
        Destroy(this);
    }
}
