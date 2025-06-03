using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Meteor : Enemy ,IDamageable
{
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
     private float speed; 
    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
        rb.velocity = Vector2.down * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void HurtSequence()
    {
        
    }

    public override void DeathSequence()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            Destroy(otherCollider.gameObject);
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

    public int Health { get; set; } = 10;

    private void OnBecameInvisible()
    {
        Destroy(this);
    }
}
