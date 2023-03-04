using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static event Action OnPlayerGotHit;

    //private bool isVulnerable;

    private void Awake()
    {
        OnPlayerGotHit += HandlePlayerGotHit;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            OnPlayerGotHit?.Invoke();
        }
    }

    private void HandlePlayerGotHit()
    {
        PlayerStats.Instance.Health -= 1;
    }
}