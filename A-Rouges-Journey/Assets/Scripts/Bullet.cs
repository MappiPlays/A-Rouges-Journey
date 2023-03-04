using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private string[] ignoredColliderNames;
    
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.velocity = transform.right * PlayerStats.Instance.AttackVelocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!ignoredColliderNames.Contains(collision.name))
            Destroy(gameObject);
    }
}
