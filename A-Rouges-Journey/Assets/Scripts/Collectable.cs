using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected CircleCollider2D coll;

    private bool pickedUp = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector2 toPlayer = new Vector2(collision.transform.position.x - rb.position.x, collision.transform.position.y - rb.position.y);
            rb.velocity += toPlayer / 4;

            if (toPlayer.magnitude < .5f)
            {
                if(pickedUp == false)
                {
                    pickedUp = true;
                    SendMessage("PickedUp");
                }
            }
        }
    }
}
