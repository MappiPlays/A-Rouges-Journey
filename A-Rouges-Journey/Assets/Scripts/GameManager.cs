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
        GameStats.OnChange += OnGameStatsChanged;
    }

    // Start is called before the first frame update
    void Start()
    {
        ui = FindObjectOfType<UIManager>().gameObject;
        exitPointer = ui.GetComponentInChildren<ExitPointer>(true).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
