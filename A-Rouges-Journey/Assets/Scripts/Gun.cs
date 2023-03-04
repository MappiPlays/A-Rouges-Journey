using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField] private bool isFiring = false;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawn;

    [SerializeField] private PlayerStats playerStats;

    private float attackDelay;
    private Vector2 inputVector;

    private void Start()
    {
        attackDelay = playerStats.AttackDelay;
    }

    void OnFire(InputValue input)
    {
        inputVector = input.Get<Vector2>();
        
        if (inputVector == Vector2.zero)
        {
            isFiring = false;
            Debug.Log("Stop Shooting!");
            return;
        }
        
        transform.SetLocalPositionAndRotation(inputVector * 0.5f, Quaternion.FromToRotation(Vector2.right, inputVector));

        if (!isFiring)
        {
            isFiring = true;
            StartCoroutine(ShootingCo());
        }

    }

    IEnumerator ShootingCo()
    {
        while(isFiring)
        {
            Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.FromToRotation(Vector2.right, inputVector));
            yield return new WaitForSeconds(attackDelay);
        }
    }
}
