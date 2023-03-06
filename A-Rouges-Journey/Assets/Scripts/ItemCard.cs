using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Card")]
public class ItemCard : ScriptableObject
{
    public string itemName;
    public Sprite artwork;
    public float movementSpeedEffect;
    public float damageEffect;
    public float delayEffect;
    public float attackSpeedEffect;

    public string EffectsToString()
    {
        string effects = "";

        if (movementSpeedEffect != 0f)
        {
            effects += "Speed " + movementSpeedEffect.ToString("+0.##;-0.##"); ;
        }
        if (damageEffect != 0f)
        {
            effects += "Damage " + damageEffect.ToString("+0.##;-0.##");
        }
        if (delayEffect != 0f)
        {
            effects += "Fire Rate " + delayEffect.ToString("+0.##;-0.##");
        }
        if (attackSpeedEffect != 0f)
        {
            effects += "Shot Speed " + attackSpeedEffect.ToString("+0.##;-0.##");
        }

        return effects;
    }
}
