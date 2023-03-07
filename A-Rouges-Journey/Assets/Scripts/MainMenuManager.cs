using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

public class MainMenuManager : MonoBehaviour
{
    private void Start()
    {
        TextMeshProUGUI textHighscore = FindObjectsOfType<TextMeshProUGUI>().Where(t => t.name == "Text_Highscore").First();
        textHighscore.SetText(PlayerPrefs.GetInt("Highscore").ToString());
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("FirstLevel");
    }
}
