using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class LevelUpScreen : MonoBehaviour
{
    [SerializeField] private Button itemLeft;
    [SerializeField] private Button itemRight;

    [SerializeField] private ItemCard[] items;

    private ItemCard item1;
    private ItemCard item2;



    private void OnEnable()
    {
        item1 = items[Random.Range(0, items.Length)];
        item2 = items[Random.Range(0, items.Length)];
        while (item1.Equals(item2))
        {
            item2 = items[Random.Range(0, items.Length)];
        }
        UpdateItemDisplay(itemLeft, item1);
        UpdateItemDisplay(itemRight, item2); ;
    }

    private void UpdateItemDisplay(Button itemDisplay, ItemCard item)
    {
        itemDisplay.GetComponentsInChildren<TextMeshProUGUI>().Where(i => i.name == "Label_ItemName").First().SetText(item.itemName);
        itemDisplay.GetComponentsInChildren<TextMeshProUGUI>().Where(i => i.name == "Label_ItemEffect").First().SetText(item.EffectsToString());
        itemDisplay.GetComponentsInChildren<Image>().Where(i => i.name == "Image_Item").First().sprite = item.artwork;
    }

    public void OnItemBought(bool isLeftItem)
    {
        Debug.Log("Item bought!");
        ItemCard item = isLeftItem ? item1 : item2;

        PlayerStats.Instance.UpdateStats(item.movementSpeedEffect, item.damageEffect, item.delayEffect, item.attackSpeedEffect);

        GameManager.Instance.ResumeGame();
    }
}
