using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
    public static event Action<BossEnemy> OnBossSpawned;
    public static event Action<float> OnBossHealthChanged;
    public static event Action OnBossDied;

    private void OnEnable()
    {
        OnBossSpawned?.Invoke(this);
    }

    protected override void OnHit(float damage)
    {
        base.OnHit(damage);
        OnBossHealthChanged?.Invoke(health);
    }

    protected override void Die()
    {
        OnBossDied?.Invoke();
        base.Die();
    }
}
