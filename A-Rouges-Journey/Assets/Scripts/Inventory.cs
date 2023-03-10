using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public static event Action<Inventory> OnChange;

    [SerializeField] private int gems;

    [SerializeField] private bool hasKey;
    public bool HasKey
    {
        get { return hasKey; }
        set 
        {
            hasKey = value;
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
    }

    public int GetGems()
    {
        return gems; 
    }

    public void AddGems(int amount)
    {
        gems += amount;
        OnChange?.Invoke(Instance);
        GameStats.Instance.Score += amount * 100;
    }

    public bool RemoveGems(int amount)
    {
        if(amount > gems)
        {
            return false;
        }
        gems -= amount;
        OnChange?.Invoke(Instance);
        return true;
    }

}
