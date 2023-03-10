using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPointer : MonoBehaviour
{
    [SerializeField] private Transform exitPoint;
    private Vector2 exitPos;
    private Vector2 playerPos;
    private Transform player;

    private void Awake()
    {
        Inventory.OnChange += OnInventoryChanged;
    }

    private void OnDestroy()
    {
        Inventory.OnChange -= OnInventoryChanged;
    }
    void Start()
    {
        gameObject.SetActive(false);
        
        exitPos = exitPoint.position;
        player = FindObjectOfType<Player>().transform;
    }

    void Update()
    {
        playerPos = player.position;
        Vector2 toExit = new Vector2(exitPos.x - playerPos.x, exitPos.y - playerPos.y).normalized;
        transform.rotation = Quaternion.FromToRotation(Vector2.up, toExit);
        transform.localPosition = toExit * 64;
    }

    private void OnInventoryChanged(Inventory inventory)
    {
        if (inventory.HasKey)
        {
            gameObject.SetActive(true);
        }
    }
}
