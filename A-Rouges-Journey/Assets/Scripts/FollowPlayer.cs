using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform player;

    private Vector2 toPlayer;

    private void Start()
    {
        rb = transform.parent.GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>().transform;
    }

    private void Update()
    {
        toPlayer = new Vector2(player.transform.position.x - rb.position.x, player.transform.position.y - rb.position.y);
        rb.velocity += toPlayer / 16;
    }
}
