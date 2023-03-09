using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Skeleton : Enemy
{
    [SerializeField] private GameObject bulletPrefab;

    private bool isWaiting;
    private bool isAttackingPlayer;
    private float speedMultiplier = 1f;
    private Vector2 targetPosition;

    protected override void Start()
    {
        base.Start();
        target = FindObjectOfType<Player>().transform;
        StartCoroutine(MoveRandomCo());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isAttackingPlayer = true;
            if(!isWaiting)
            {
                StartCoroutine(ShootCo());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isAttackingPlayer = false;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * movementSpeed * speedMultiplier));
    }

    IEnumerator ShootCo()
    {
        Vector2 toTarget;
        Quaternion rotation;
        while (isAttackingPlayer)
        {
            toTarget = new Vector2(transform.position.x - target.position.x, transform.position.y - target.position.y) * -1;
            rotation = Quaternion.FromToRotation(Vector2.right, toTarget);
            Instantiate(bulletPrefab, transform.position, rotation);
            isWaiting = true;
            yield return new WaitForSeconds(2);
            isWaiting = false;
        }
    }

    IEnumerator MoveRandomCo()
    {
        int x, y;
        while (true)
        {
            x = Random.Range(-1, 2);
            y = Random.Range(-1, 2);
            targetPosition = new Vector2(transform.position.x + x, transform.position.y + y);
            anim.SetFloat("MovementX", targetPosition.x - transform.position.x);
            anim.SetFloat("MovementY", targetPosition.y - transform.position.y);
            anim.SetBool("MagXGreaterMagY", Mathf.Abs(anim.GetFloat("MovementX")) > Mathf.Abs(anim.GetFloat("MovementY")));
            speedMultiplier = 1f;
            anim.SetBool("IsMoving", true);
            yield return new WaitForSeconds(1);
            speedMultiplier = 0f;
            targetPosition = transform.position;
            anim.SetFloat("MovementX", targetPosition.x - transform.position.x);
            anim.SetFloat("MovementY", targetPosition.y - transform.position.y);
            anim.SetTrigger("ResetDirection");
            anim.SetBool("IsMoving", false);
            yield return new WaitForSeconds(Random.Range(0, 5));
        }
    }
}
