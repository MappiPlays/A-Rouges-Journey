using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    public static GameStats Instance;

    public static event Action<GameStats> OnChange;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //OnChange?.Invoke(Instance);
    }

    [SerializeField] private int numOfEnemies;
    public int NumOfEnemies
    {
        get { return numOfEnemies; }
        set { 
            numOfEnemies = value; 
            OnChange?.Invoke(Instance);
        }
    }
}
