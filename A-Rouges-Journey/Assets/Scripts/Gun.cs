using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField] private bool isFiring = false, isCooldown = false;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawn;

    private float attackDelay;
    private Vector2 inputVector;

    private void Awake()
    {
        PlayerStats.OnChange += UpdateAttackDelay;
    }

    private void OnDestroy()
    {
        PlayerStats.OnChange -= UpdateAttackDelay;
    }

    private void Start()
    {
        attackDelay = PlayerStats.Instance.AttackDelay;
    }

    void OnFire(InputValue input)
    {
        inputVector = input.Get<Vector2>();
        
        if (inputVector == Vector2.zero)
        {
            isFiring = false;
            return;
        }
        
        transform.SetLocalPositionAndRotation(inputVector * 0.5f, Quaternion.FromToRotation(Vector2.right, inputVector));

        if (!isFiring)
        {
            isFiring = true;
            if(!isCooldown)
                StartCoroutine(ShootingCo());
        }

    }

    IEnumerator ShootingCo()
    {
        while(isFiring)
        {
            Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.FromToRotation(Vector2.right, inputVector));
            isCooldown = true;
            yield return new WaitForSeconds(1/attackDelay);
            isCooldown = false;
        }
    }

    void UpdateAttackDelay(PlayerStats stats)
    {
        attackDelay = stats.AttackDelay;
    }
}
