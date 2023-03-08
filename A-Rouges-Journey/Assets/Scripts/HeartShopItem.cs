using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartShopItem : ShopItem
{
    protected override void ApplyItemEffect()
    {
        base.ApplyItemEffect();
        PlayerStats.Instance.Health += 2;
    }
}
