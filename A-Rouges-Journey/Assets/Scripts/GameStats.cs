using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    public static GameStats Instance;

    public static event Action<GameStats> OnStatsChange;
    public static event Action<int> OnScoreChange;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //OnChange?.Invoke(Instance);
        score = 500;
        OnScoreChange?.Invoke(score);
        StartCoroutine(scoreDecreaseOverTimeCo());
    }

    [SerializeField] private int numOfEnemies;
    public int NumOfEnemies
    {
        get { return numOfEnemies; }
        set { 
            numOfEnemies = value; 
            OnStatsChange?.Invoke(Instance);
        }
    }

    [SerializeField] private int score;
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            OnScoreChange?.Invoke(score);
        }
    }

    IEnumerator scoreDecreaseOverTimeCo()
    {
        while(Score > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            Score--;
        }
    }
}
