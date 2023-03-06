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

    

    private void OnEnable()
    {
        ItemCard item1 = items[Random.Range(0, items.Length)];
        ItemCard item2 = items[Random.Range(0, items.Length)];
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
        itemDisplay.GetComponentsInChildren<Image>().Where(i => i.name == "Image_Item").First().sprite = item.artwork;
    }

    public void OnItemBought()
    {
        Debug.Log("Item bought!");
        GameManager.Instance.ResumeGame();
    }
}
