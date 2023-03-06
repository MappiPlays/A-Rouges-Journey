using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;

public class UIManager : MonoBehaviour
{
    private GameObject levelUpScreen;
    private TextMeshProUGUI[] UITexts;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI gemsText;
    private TextMeshProUGUI speedText;
    private TextMeshProUGUI damageText;
    private TextMeshProUGUI delayText;
    private TextMeshProUGUI attackSpeedText;
    private TextMeshProUGUI playerLevelText;
    private Slider xpBar;

    private void Awake()
    {
        PlayerStats.OnChange += UpdateStatsUI;
        PlayerStats.OnLevelUp += ShowLevelUpScreen;
        Inventory.OnChange += UpdateInventoryUI;
        GameStats.OnScoreChange += UpdateScoreUI;
        levelUpScreen = GetComponentInChildren<LevelUpScreen>().gameObject;
        levelUpScreen.SetActive(false);
        UITexts = GetComponentsInChildren<TextMeshProUGUI>();
        scoreText = UITexts.Where(t => t.name == "Text_Score").First();
        gemsText = UITexts.Where(t => t.name == "Text_Gems").First();
        speedText = UITexts.Where(t => t.name == "Text_Speed").First();
        damageText = UITexts.Where(t => t.name == "Text_Damage").First();
        delayText = UITexts.Where(t => t.name == "Text_Delay").First();
        attackSpeedText = UITexts.Where(t => t.name == "Text_AttackSpeed").First();
        playerLevelText = UITexts.Where(t => t.name == "Text_PlayerLevel").First();
        xpBar = GetComponentInChildren<Slider>();
    }

    private void OnDestroy()
    {
        PlayerStats.OnChange -= UpdateStatsUI;
        PlayerStats.OnLevelUp -= ShowLevelUpScreen;
        Inventory.OnChange -= UpdateInventoryUI;
        GameStats.OnScoreChange -= UpdateScoreUI;
    }

    private void UpdateStatsUI(PlayerStats stats)
    {
        speedText.SetText(stats.MovementSpeed.ToString("F2", CultureInfo.InvariantCulture));
        damageText.SetText(stats.AttackDamage.ToString("F2", CultureInfo.InvariantCulture));
        delayText.SetText(stats.AttackDelay.ToString("F2", CultureInfo.InvariantCulture));
        attackSpeedText.SetText(stats.AttackVelocity.ToString("F2", CultureInfo.InvariantCulture));
        playerLevelText.SetText(stats.PlayerLevel.ToString());
        xpBar.maxValue = stats.ExperienceToLevelUp;
        xpBar.value = stats.Experience;
    }

    private void UpdateInventoryUI(Inventory inventory)
    {
        gemsText.SetText(inventory.GetGems().ToString());
    }

    private void UpdateScoreUI(int score)
    {
        scoreText.SetText(score.ToString());
    }

    private void ShowLevelUpScreen()
    {
        levelUpScreen.SetActive(true);
    }
}
