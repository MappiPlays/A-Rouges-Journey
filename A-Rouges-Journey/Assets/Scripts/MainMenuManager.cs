using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Animator fadeoutAnim;

    private void Start()
    {
        TextMeshProUGUI textHighscore = FindObjectsOfType<TextMeshProUGUI>().Where(t => t.name == "Text_Highscore").First();
        textHighscore.SetText(PlayerPrefs.GetInt("Highscore").ToString());
    }

    public void LoadGame()
    {
        StartCoroutine(LoadScene("FirstLevel"));
    }

    IEnumerator LoadScene(string scenename)
    {
        fadeoutAnim.SetTrigger("Fadeout");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(scenename);
    }
}
