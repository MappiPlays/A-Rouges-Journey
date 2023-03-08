using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

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
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        PlayerStats.OnLevelUp += FreezGame;
        SceneManager.sceneLoaded += HandleSceneLoaded;
        PlayerStats.OnPlayerDied += HandlePlayerDied;
        Time.timeScale = 1f;
        FindReferences();
    }

    private void OnDestroy()
    {
        PlayerStats.OnLevelUp -= FreezGame;
        SceneManager.sceneLoaded -= HandleSceneLoaded;
    }

    private void FindReferences()
    {
        ui = FindObjectOfType<UIManager>().gameObject;
        exitPointer = ui.GetComponentInChildren<ExitPointer>(true).gameObject;
        Tilemap[] tilemaps = FindObjectsOfType<Tilemap>(true);
        if(tilemaps.Length > 0)
            exitBorder = tilemaps.Where(t => t.gameObject.name == "ExitBorder").FirstOrDefault().gameObject;
    }

    private void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(!(scene.name == "EndScreen" || scene.name == "MainMenu" || scene.name == "DeathScreen"))
            FindReferences();
    }

    private void HandlePlayerDied()
    {
        FreezGame();
        LoadNewScene("DeathScreen");
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

    public void LoadNewScene(string sceneName)
    {
        GameStats.Instance.NumOfEnemies = 0;
        SceneManager.LoadScene(sceneName);
    }

    public void EndRun()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void FinishGame()
    {
        if(PlayerPrefs.GetInt("HighScore") < GameStats.Instance.Score)
        {
            PlayerPrefs.SetInt("HighScore", GameStats.Instance.Score);
        }
    }
}
