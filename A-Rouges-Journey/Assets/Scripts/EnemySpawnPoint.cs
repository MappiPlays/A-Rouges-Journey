using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject[] enemiePrefabs;


    private void Start()
    {
        int enemynum = Random.Range(0, enemiePrefabs.Length);
        Instantiate(enemiePrefabs[enemynum], transform);
    }
}
