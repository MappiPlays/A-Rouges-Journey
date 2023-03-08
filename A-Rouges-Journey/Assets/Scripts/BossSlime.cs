using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSlime : BossEnemy
{
    [SerializeField] private GameObject bulletPrefab;

    protected override void Start()
    {
        base.Start();
        target = FindObjectOfType<Player>().transform;
        StartCoroutine(AttackCo());
    }

    IEnumerator AttackCo()
    {
        while(health > 0)
        {
            yield return new WaitForSeconds(1);
            RandomAttack();
        }
    }

    private void RandomAttack()
    {
        switch(Random.Range(0, 2))
        {
            case 0:
                StartCoroutine(Shoot4Directions());
                break;
            case 1:
                StartCoroutine(SlideAttack());
                break;
        }
    }

    IEnumerator Shoot4Directions()
    {
        int rand = Random.Range(0, 3);
        for (int i = 0; i < rand; i++)
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, 0));
            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, 90));
            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, 180));
            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, -90));
            yield return new WaitForSeconds(.5f);
        }
    }

    IEnumerator SlideAttack()
    {
        Vector2 toTarget = new Vector2(transform.position.x - target.position.x, transform.position.y - target.position.y).normalized * -1;
        rb.velocity = (toTarget * movementSpeed);
        yield return new WaitForSeconds(1.5f);
    }
}
