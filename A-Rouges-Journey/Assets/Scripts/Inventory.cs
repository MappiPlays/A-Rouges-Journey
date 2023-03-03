using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int gems;
    
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
