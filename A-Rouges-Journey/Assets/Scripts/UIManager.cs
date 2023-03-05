using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] statsUITexts;

    private TextMeshProUGUI gemsText;
    private TextMeshProUGUI speedText;
    private TextMeshProUGUI damageText;
    private TextMeshProUGUI delayText;
    private TextMeshProUGUI attackSpeedText;

    private void Awake()
    {
        PlayerStats.OnChange += UpdateStatsUI;
        Inventory.OnChange += UpdateInventoryUI;
        statsUITexts = GetComponentsInChildren<TextMeshProUGUI>();
        gemsText = statsUITexts.Where(t => t.name == "Text_Gems").First();
        speedText = statsUITexts.Where(t => t.name == "Text_Speed").First();
        damageText = statsUITexts.Where(t => t.name == "Text_Damage").First();
        delayText = statsUITexts.Where(t => t.name == "Text_Delay").First();
        attackSpeedText = statsUITexts.Where(t => t.name == "Text_AttackSpeed").First();
    }

    private void OnDestroy()
    {
        PlayerStats.OnChange -= UpdateStatsUI;
        Inventory.OnChange -= UpdateInventoryUI;
    }

    private void UpdateStatsUI(PlayerStats stats)
    {
        speedText.SetText(stats.MovementSpeed.ToString("F2", CultureInfo.InvariantCulture));
        damageText.SetText(stats.AttackDamage.ToString("F2", CultureInfo.InvariantCulture));
        delayText.SetText(stats.AttackDelay.ToString("F2", CultureInfo.InvariantCulture));
        attackSpeedText.SetText(stats.AttackVelocity.ToString("F2", CultureInfo.InvariantCulture));
    }

    private void UpdateInventoryUI(Inventory inventory)
    {
        gemsText.SetText(inventory.GetGems().ToString());
    }
}
