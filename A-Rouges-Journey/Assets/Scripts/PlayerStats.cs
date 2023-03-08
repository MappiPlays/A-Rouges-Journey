using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    public static event Action<PlayerStats> OnChange;
    public static event Action OnLevelUp;
    public static event Action OnPlayerDied;

    [SerializeField] private int playerLevel;
    public int PlayerLevel
    {
        get { return playerLevel; }
        set
        {
            playerLevel = value;
            OnLevelUp?.Invoke();
        }
    }

    [SerializeField] private int experience;
    public int Experience
    {
        get { return experience; }
        set
        {
            experience = value;
            OnChange?.Invoke(Instance);
            if(experience >= experienceToLevelUp)
            {
                PlayerLevel++;
            }
        }
    }

    [SerializeField] private int experienceToLevelUp;
    public int ExperienceToLevelUp
    {
        get { return experienceToLevelUp; }
        set
        {
            experienceToLevelUp = value;
            OnChange?.Invoke(Instance);
        }
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
            if(health > 6)
            {
                health = 6;
            }
            OnChange?.Invoke(Instance);
            if(health <= 0)
            {
                OnPlayerDied?.Invoke();
            }
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


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        OnLevelUp += HandleLevelUp;
    }

    private void OnEnable()
    {
        OnChange?.Invoke(Instance);
    }

    private void Start()
    {
        OnChange?.Invoke(Instance);
    }

    private void OnDestroy()
    {
        OnLevelUp -= HandleLevelUp;
    }

    private void HandleLevelUp()
    {
        Experience = Experience - experienceToLevelUp;
        experienceToLevelUp = Mathf.RoundToInt(experienceToLevelUp * 1.5f);
    }

    public void UpdateStats(float moveSpeed, float damage, float delay, float attackSpeed)
    {
        movementspeed += moveSpeed;
        attackdamage += damage;
        attackdelay += delay;
        attackVelocity += attackSpeed;
        OnChange?.Invoke(Instance);
    }
}
