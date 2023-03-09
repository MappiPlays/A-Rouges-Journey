using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [SerializeField] protected int price;
    [SerializeField] protected bool canApplyEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SendMessage("RefreshCanApplyEffect");
            if(Inventory.Instance.GetGems() >= price && canApplyEffect)
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

    protected virtual void RefreshCanApplyEffect()
    { 
    
    }
}
