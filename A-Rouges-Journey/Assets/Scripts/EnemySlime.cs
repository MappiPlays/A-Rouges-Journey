using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float movementSpeed;
    [SerializeField] private Transform target;
    [SerializeField] private GameObject drop;
    [SerializeField] private float dropChance;

    private float speedMultiplier;
    private Rigidbody2D rb;
    private Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = transform.parent;
        speedMultiplier = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            target = collision.transform;
            speedMultiplier = 1f;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(Vector2.MoveTowards(transform.position, target.position, Time.deltaTime * movementSpeed * speedMultiplier));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            target = transform.parent;
            speedMultiplier = .5f;
        }
    }

    private void OnHit(float damage)
    {
        anim.SetTrigger("TakeDamage");
        health -= damage;
        if(health <= 0f)
        {
            if(Random.value <= dropChance)
            {
                Instantiate(drop, transform.position, Quaternion.identity);
            }
            Die();
        }
    }

    private void Die()
    {
        GameStats.Instance.NumOfEnemies--;
        Destroy(transform.parent.gameObject);
        Destroy(gameObject);
    }
}
