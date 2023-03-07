using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static event Action OnPlayerGotHit;

    private Animator anim;

    private void Awake()
    {
        OnPlayerGotHit += HandlePlayerGotHit;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        OnPlayerGotHit -= HandlePlayerGotHit;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            OnPlayerGotHit?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            OnPlayerGotHit?.Invoke();
        }
    }

    private void HandlePlayerGotHit()
    {
        PlayerStats.Instance.Health -= 1;
        anim.SetTrigger("TakeDamage");
    }

    private void OnEscape()
    {
        GameManager.Instance.PauseGame();
    }
}