using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private string[] ignoredColliderTags;
    [SerializeField] private float velocity;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!ignoredColliderTags.Contains(collision.tag))
        {
            Destroy(gameObject);
        }
    }

}
