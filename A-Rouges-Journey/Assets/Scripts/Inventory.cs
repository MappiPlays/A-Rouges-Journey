using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    [SerializeField] private int gems;

    private void Awake()
    {
        Instance = this;
    }

    public int GetGems()
    {
        return gems; 
    }

    public void AddGems(int amount)
    {
        gems += amount;
    }

    public bool RemoveGems(int amount)
    {
        if(amount > gems)
        {
            return false;
        }
        gems -= amount;
        return true;
    }

}
