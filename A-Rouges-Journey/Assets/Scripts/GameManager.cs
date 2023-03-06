using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    [SerializeField] private GameObject ui;
    [SerializeField] private GameObject exitBorder;
    [SerializeField] private GameObject exitPointer;

    public static event Action OnGamePaused;
    public static event Action OnGameFreezed;
    public static event Action OnGameResumed;

    private void Awake()
    {
        Instance = this;
        GameStats.OnStatsChange += OnGameStatsChanged;
        PlayerStats.OnLevelUp += FreezGame;
    }

    private void OnDestroy()
    {
        GameStats.OnStatsChange -= OnGameStatsChanged;
        PlayerStats.OnLevelUp -= FreezGame;
    }

    // Start is called before the first frame update
    void Start()
    {
        ui = FindObjectOfType<UIManager>().gameObject;
        exitPointer = ui.GetComponentInChildren<ExitPointer>(true).gameObject;
    }

    private void OnGameStatsChanged(GameStats stats)
    {
        if(stats.NumOfEnemies == 0) 
        {
            Debug.Log("All Enemies are Dead");
            exitPointer.SetActive(true);
            exitBorder.SetActive(false);
        }
    }

    public void PauseGame()
    {
        FreezGame();
        OnGamePaused?.Invoke();
    }

    public void FreezGame()
    {
        Time.timeScale = 0f;
        OnGameFreezed?.Invoke();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        OnGameResumed?.Invoke();
    }
}
