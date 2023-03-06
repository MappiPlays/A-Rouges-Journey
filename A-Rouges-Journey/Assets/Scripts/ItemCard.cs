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
}
