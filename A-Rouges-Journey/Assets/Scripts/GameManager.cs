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

    private void Awake()
    {
        Instance = this;
        GameStats.OnStatsChange += OnGameStatsChanged;
        PlayerStats.OnLevelUp += PauseGame;
    }

    private void OnDestroy()
    {
        GameStats.OnStatsChange -= OnGameStatsChanged;
        PlayerStats.OnLevelUp -= PauseGame;
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
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }
}