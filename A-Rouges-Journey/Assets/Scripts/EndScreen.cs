using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private TextMeshProUGUI labelHighscore;

    private void Start()
    {
        TextMeshProUGUI[] texts = FindObjectsOfType<TextMeshProUGUI>();
        textScore = texts.Where(t => t.name == "Text_Score").First();
        labelHighscore = texts.Where(t => t.name == "Label_HighScore").First();
        labelHighscore.gameObject.SetActive(false);

        if (GameStats.Instance != null)
        {
            int score = GameStats.Instance.Score;
            textScore.SetText(score.ToString());
            if(score > PlayerPrefs.GetInt("Highscore"))
            {
                labelHighscore.gameObject.SetActive(true);
                PlayerPrefs.SetInt("Highscore", score);
            }
        }
    }

    public void ReturnToMainMenu()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.EndRun();
        else
            SceneManager.LoadScene(0);
    }
    
}
