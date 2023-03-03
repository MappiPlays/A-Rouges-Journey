using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveDirection;
    
    private Rigidbody2D rb;
    private PlayerStats stats;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        rb.MovePosition(rb.position + stats.MovementSpeed * Time.fixedDeltaTime * moveDirection);
    }

    void OnMove(InputValue input)
    {
        moveDirection = input.Get<Vector2>();
    }
}
