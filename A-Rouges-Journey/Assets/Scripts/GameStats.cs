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
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        GameManager.OnGameFreezed += HandleGameFreezed;
        GameManager.OnGameResumed += HandleGameResumed;
    }

    private void OnDestroy()
    {
        GameManager.OnGameFreezed -= HandleGameFreezed;
        GameManager.OnGameResumed -= HandleGameResumed;
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

    [SerializeField] private bool hasBoss;
    public bool HasBoss
    {
        get { return hasBoss; }
        set
        {
            hasBoss = value;
            OnStatsChange?.Invoke(Instance);
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

    private void HandleGameFreezed()
    {
        StopAllCoroutines();
    }

    private void HandleGameResumed()
    {
        StartCoroutine(scoreDecreaseOverTimeCo());
    }

}
