using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenKey : Collectable
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "ExitBorder") 
        {
            collision.gameObject.SetActive(false);
            FindObjectOfType<ExitPointer>().gameObject.SetActive(false);
            Inventory.Instance.HasKey = false;
            Destroy(gameObject);
        }
    }

    private void PickedUp()
    {
        Inventory.Instance.HasKey = true;
        FindObjectOfType<FollowPlayer>(true).gameObject.SetActive(true);
    }
}
