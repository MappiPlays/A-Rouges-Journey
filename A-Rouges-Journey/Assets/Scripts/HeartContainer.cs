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
        PlayerStats.OnChange += UpdateSprite;
        img = GetComponent<Image>();
    }

    private void Start()
    {
        UpdateSprite(PlayerStats.Instance);
    }

    private void OnDestroy()
    {
        PlayerStats.OnChange -= UpdateSprite;
    }

    public void UpdateSprite(PlayerStats stats)
    {
        switch (stats.Health - (2 * heartNum))
        {
            case <= 0:
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
