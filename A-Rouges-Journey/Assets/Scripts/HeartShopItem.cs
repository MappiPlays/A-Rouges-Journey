using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartShopItem : ShopItem
{
    protected override void ApplyItemEffect()
    {
        base.ApplyItemEffect();
        if (canApplyEffect)
            PlayerStats.Instance.Health += 2;
    }

    protected override void RefreshCanApplyEffect()
    {
        base.RefreshCanApplyEffect();
        canApplyEffect = PlayerStats.Instance.Health < 6;
    }
}
