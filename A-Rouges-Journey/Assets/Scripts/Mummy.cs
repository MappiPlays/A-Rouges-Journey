using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mummy : Enemy
{
    private float speedMultiplier = 1f;
    private Vector2 targetPosition;
    private bool playerFocused;
    private bool isMoving;
    private bool isSlow;

    private Coroutine movingCo;

    protected override void Start()
    {
        base.Start();
        target = FindObjectOfType<Player>().GetComponent<Transform>();
        StartCoroutine(MoveRandomCo());
    }

    private void FixedUpdate()
    {
        if(playerFocused && isSlow)
        {
            rb.MovePosition(Vector2.MoveTowards(transform.position, target.position, Time.deltaTime * movementSpeed * speedMultiplier));
            anim.SetFloat("MovementX", target.position.x - transform.position.x);
            anim.SetFloat("MovementY", target.position.y - transform.position.y);
            anim.SetBool("MagXGreaterMagY", Mathf.Abs(anim.GetFloat("MovementX")) > Mathf.Abs(anim.GetFloat("MovementY")));
        }
        else
        {
            rb.MovePosition(Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * movementSpeed * speedMultiplier));
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(isMoving == false) 
            {
                playerFocused = true;
                targetPosition = collision.transform.position + new Vector3(Random.Range(-1, 2), Random.Range(-1, 2));
                anim.SetFloat("MovementX", targetPosition.x - transform.position.x);
                anim.SetFloat("MovementY", targetPosition.y - transform.position.y);
                anim.SetBool("MagXGreaterMagY", Mathf.Abs(anim.GetFloat("MovementX")) > Mathf.Abs(anim.GetFloat("MovementY")));
                movingCo = StartCoroutine(MoveForSeconds(1f));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerFocused = false;
            StartCoroutine(MoveRandomCo());
        }
    }

    IEnumerator MoveRandomCo()
    {
        int x, y;
        while (!playerFocused)
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
            anim.SetTrigger("ResetDirection");
            anim.SetBool("IsMoving", false);
            yield return new WaitForSeconds(Random.Range(0, 5));
        }
    }

    IEnumerator MoveForSeconds(float seconds)
    {
        isMoving = true;
        anim.SetBool("IsMoving", true);
        speedMultiplier = 4f;
        yield return new WaitForSeconds(seconds);
        speedMultiplier = .5f;
        anim.SetTrigger("ResetDirection");
        isSlow = true;
        yield return new WaitForSeconds(2f);
        isSlow = false;
        isMoving = false;
        anim.SetBool("IsMoving", false);
    }

}
