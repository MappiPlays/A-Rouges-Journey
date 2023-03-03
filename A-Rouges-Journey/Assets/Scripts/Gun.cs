using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField] private bool isFiring = false;

    void OnFire(InputValue input)
    {
        Vector2 inputVector = input.Get<Vector2>();
        
        if (inputVector == Vector2.zero)
        {
            isFiring = false;
            Debug.Log("Stop Shooting!");
            return;
        }
        
        transform.SetPositionAndRotation(inputVector * 0.75f, Quaternion.FromToRotation(Vector2.right, inputVector));

        if (!isFiring)
        {
            isFiring = true;
            Debug.Log("Shooting Start!");
        }

    }
}
