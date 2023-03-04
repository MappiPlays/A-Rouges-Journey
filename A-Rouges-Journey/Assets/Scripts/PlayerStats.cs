using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    public static event Action<PlayerStats> OnChange;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        OnChange?.Invoke(Instance);
    }

    [SerializeField] private float movementspeed;
    public float MovementSpeed
    {
        get { return movementspeed; }
        set
        { 
            movementspeed = value;
            OnChange?.Invoke(Instance);
        }
    }

    [SerializeField] private int health;
    public int Health
    {
        get { return health; }
        set 
        { 
            health = value;
            OnChange?.Invoke(Instance);
        }
    }

    [SerializeField] private float attackdelay;
    public float AttackDelay
    {
        get { return attackdelay; }
        set
        { 
            attackdelay = value;
            OnChange?.Invoke(Instance);
        }
    }

    [SerializeField] private float attackVelocity;
    public float AttackVelocity
    {
        get { return attackVelocity; }
        set 
        { 
            attackVelocity = value;
            OnChange?.Invoke(Instance);
        }
    }

    [SerializeField] private float attackdamage;
    public float AttackDamage
    {
        get { return attackdamage; }
        set
        {
            attackdamage = value;
            OnChange?.Invoke(Instance);
        }
    }
}
