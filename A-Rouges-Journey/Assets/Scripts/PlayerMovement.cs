using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveDirection;
    
    private Rigidbody2D rb;
    private Animator playerAnimator;
    private float moveSpeed;

    private void Awake()
    {
        PlayerStats.OnChange += UpdateMoveSpeed;
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        PlayerStats.OnChange -= UpdateMoveSpeed;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveSpeed * Time.deltaTime * moveDirection);
        playerAnimator.SetFloat("MovementX", moveDirection.x);
        playerAnimator.SetFloat("MovementY", moveDirection.y);
    }

    void OnMove(InputValue input)
    {
        moveDirection = input.Get<Vector2>();
    }

    private void UpdateMoveSpeed(PlayerStats stats)
    {
        moveSpeed = stats.MovementSpeed;
    }
}
