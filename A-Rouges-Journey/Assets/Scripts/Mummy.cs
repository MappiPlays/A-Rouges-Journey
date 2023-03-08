using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mummy : Enemy
{
    private float speedMultiplier = 1;
    private Vector2 targetPosition;
    private bool playerFocused;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(MoveRandomCo());
    }

    private void FixedUpdate()
    {
        rb.MovePosition(Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * movementSpeed * speedMultiplier));
    }

    IEnumerator MoveRandomCo()
    {
        int x, y;
        while (!playerFocused)
        {
            x = Random.Range(-1, 2);
            y = Random.Range(-1, 2);
            targetPosition = new Vector2(transform.position.x + x, transform.position.y + y);
            yield return new WaitForSeconds(1);
            targetPosition = transform.position;
            yield return new WaitForSeconds(Random.Range(0, 5));
        }

    }
}
