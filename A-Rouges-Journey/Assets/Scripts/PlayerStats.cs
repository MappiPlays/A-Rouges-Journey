using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float movementspeed;
    public float MovementSpeed
    {
        get { return movementspeed; }
        set { movementspeed = value; }
    }

    [SerializeField] private int health;
    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    [SerializeField] private float attackdelay;
    public float AttackDelay
    {
        get { return attackdelay; }
        set { attackdelay = value; }
    }

    [SerializeField] private float attackVelocity;
    public float AttackVelocity
    {
        get { return attackVelocity; }
        set { attackVelocity = value; }
    }
}
