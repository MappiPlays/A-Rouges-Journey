using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [SerializeField] protected int price;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(Inventory.Instance.GetGems() >= price)
            {
                Inventory.Instance.RemoveGems(price);
                SendMessage("ApplyItemEffect");
                Destroy(gameObject);
            }
        }
    }

    protected virtual void ApplyItemEffect()
    {

    }
}
