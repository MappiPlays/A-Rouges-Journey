using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartContainer : MonoBehaviour
{
    [SerializeField] private int heartNum;
    [SerializeField] private Sprite spriteFull;
    [SerializeField] private Sprite spriteHalf;
    [SerializeField] private Sprite spriteEmpty;

    public PlayerStats stats;
    private Image img;

    private void Awake()
    {
        img = GetComponent<Image>();
    }

    private void Start()
    {
        updateSprite();
    }

    public void updateSprite()
    {
        switch (stats.Health - (2 * heartNum))
        {
            case 0:
                img.sprite = spriteEmpty;
                break;
            
            case 1:
                img.sprite = spriteHalf;
                break;

            case 2:
                img.sprite = spriteFull;
                break;
        }
    }
}
