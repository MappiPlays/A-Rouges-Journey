using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected float movementSpeed;
    [SerializeField] protected Transform target;
    [SerializeField] protected GameObject drop;
    [SerializeField] protected float dropChance;
    [SerializeField] protected int scoreOnDeath;

    private void Awake()
    {
        GameStats.Instance.NumOfEnemies++;
    }

    protected void OnHit(float damage)
    {
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
