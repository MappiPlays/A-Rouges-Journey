using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float health;
    [SerializeField] protected float movementSpeed;
    [SerializeField] protected Transform target;
    [SerializeField] protected GameObject drop;
    [SerializeField] protected float dropChance;
    [SerializeField] protected int scoreOnDeath;

    protected Rigidbody2D rb;
    protected Animator anim;

    virtual protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    virtual protected void Start()
    {
        GameStats.Instance.NumOfEnemies++;
    }

    virtual protected void OnHit(float damage)
    {
        anim.SetTrigger("TakeDamage");
        health -= damage;
        if(health <= 0f)
        {
            Die();
        }
    }

    virtual protected void Die()
    {
        if (Random.value <= dropChance)
        {
            Instantiate(drop, transform.position, Quaternion.identity);
        }
        GameStats.Instance.Score += scoreOnDeath;
        PlayerStats.Instance.Experience += scoreOnDeath;
        GameStats.Instance.NumOfEnemies--;
        Destroy(gameObject);
    }
}
