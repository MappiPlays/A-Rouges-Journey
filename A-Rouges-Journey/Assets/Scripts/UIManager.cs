using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;
using Mono.Cecil;

public class UIManager : MonoBehaviour
{
    private GameObject levelUpScreen;
    private GameObject PauseScreen;
    private TextMeshProUGUI[] UITexts;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI gemsText;
    private TextMeshProUGUI speedText;
    private TextMeshProUGUI damageText;
    private TextMeshProUGUI delayText;
    private TextMeshProUGUI attackSpeedText;
    private TextMeshProUGUI playerLevelText;
    private TextMeshProUGUI xpGainedText;
    private Slider xpBar;
    private Slider bossBar;

    private void Awake()
    {
        PlayerStats.OnChange += UpdateStatsUI;
        PlayerStats.OnLevelUp += ShowLevelUpScreen;
        Inventory.OnChange += UpdateInventoryUI;
        GameStats.OnScoreChange += UpdateScoreUI;
        GameManager.OnGamePaused += HandleGamePaused;
        BossEnemy.OnBossSpawned += ActivateBossBar;
        BossEnemy.OnBossHealthChanged += UpdateBossBar;
        BossEnemy.OnBossDied += DeactivateBossBar;
        UITexts = GetComponentsInChildren<TextMeshProUGUI>(true);
        levelUpScreen = GetComponentInChildren<LevelUpScreen>(true).gameObject;
        PauseScreen = UITexts.Where(t => t.name == "Label_Pause").First().transform.parent.parent.gameObject;
        scoreText = UITexts.Where(t => t.name == "Text_Score").First();
        gemsText = UITexts.Where(t => t.name == "Text_Gems").First();
        speedText = UITexts.Where(t => t.name == "Text_Speed").First();
        damageText = UITexts.Where(t => t.name == "Text_Damage").First();
        delayText = UITexts.Where(t => t.name == "Text_Delay").First();
        attackSpeedText = UITexts.Where(t => t.name == "Text_AttackSpeed").First();
        playerLevelText = UITexts.Where(t => t.name == "Text_PlayerLevel").First();
        xpGainedText = UITexts.Where(t => t.name == "Text_xpGained").First();
        xpBar = GetComponentsInChildren<Slider>().Where(s => s.name == "xpBar").First();
        bossBar = GetComponentsInChildren<Slider>(true).Where(s => s.name == "BossBar").First();
        
        levelUpScreen.SetActive(false);
        PauseScreen.SetActive(false);
        bossBar.gameObject.SetActive(false);
    }

    private void Start()
    {
        UpdateStatsUI(PlayerStats.Instance);
        UpdateInventoryUI(Inventory.Instance);
        UpdateScoreUI(GameStats.Instance.Score);
    }

    private void OnDestroy()
    {
        PlayerStats.OnChange -= UpdateStatsUI;
        PlayerStats.OnLevelUp -= ShowLevelUpScreen;
        Inventory.OnChange -= UpdateInventoryUI;
        GameStats.OnScoreChange -= UpdateScoreUI;
        GameManager.OnGamePaused -= HandleGamePaused;
        BossEnemy.OnBossSpawned -= ActivateBossBar;
        BossEnemy.OnBossHealthChanged -= UpdateBossBar;
        BossEnemy.OnBossDied -= DeactivateBossBar;
    }

    private void UpdateStatsUI(PlayerStats stats)
    {
        int xpChange = stats.Experience - (int) xpBar.value;
        speedText.SetText(stats.MovementSpeed.ToString("F2", CultureInfo.InvariantCulture));
        damageText.SetText(stats.AttackDamage.ToString("F2", CultureInfo.InvariantCulture));
        delayText.SetText(stats.AttackDelay.ToString("F2", CultureInfo.InvariantCulture));
        attackSpeedText.SetText(stats.AttackVelocity.ToString("F2", CultureInfo.InvariantCulture));
        playerLevelText.SetText(stats.PlayerLevel.ToString());
        xpBar.maxValue = stats.ExperienceToLevelUp;
        xpBar.value = stats.Experience;
        if(xpChange > 0)
            StartCoroutine(ShowXpGained(xpChange));
    }

    IEnumerator ShowXpGained(int value)
    {
        Animator xpTextAnim = xpGainedText.GetComponent<Animator>();
        xpGainedText.SetText("+" + value.ToString() + " XP");
        xpTextAnim.SetTrigger("Show");
        yield return new WaitForSeconds(1f);
        xpTextAnim.SetTrigger("FadeOut");
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

    private void HandleGamePaused()
    {
        PauseScreen.SetActive(true);
    }

    private void ActivateBossBar(BossEnemy boss)
    {
        bossBar.maxValue = boss.health;
        bossBar.value = boss.health;
        bossBar.gameObject.SetActive(true);
    }

    private void UpdateBossBar(float health)
    {
        bossBar.value = health;
    }

    private void DeactivateBossBar()
    {
        bossBar.gameObject.SetActive(false);
    }
}
