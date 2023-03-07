using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : Enemy
{
    private float speedMultiplier;

    protected override void Start()
    {
        target = transform.parent;
        speedMultiplier = 0f;
        base.Start();
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

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            target = collision.transform;
        }
    }

}
