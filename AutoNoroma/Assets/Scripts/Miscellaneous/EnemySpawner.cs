using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float timeCountdown;
    public float spawnRateMin;
    public float spawnRateMax;
    public Transform[] spawnPoints;
    public int spawnIndex;

    float spawnTimeRate;

    private void Start()
    {
        spawnTimeRate = Random.Range(spawnRateMin, spawnRateMax);
        timeCountdown = spawnTimeRate;
    }
    private void Update()
    {
        timeCountdown -= Time.deltaTime;
        if(timeCountdown < 0 )
        {
            spawnTimeRate = Random.Range(spawnRateMin, spawnRateMax);
            spawnIndex = Random.Range(0, spawnPoints.Length);
            ObjectPool.Instance.SpawnFromPool("Enemy", 
                spawnPoints[spawnIndex].position, Quaternion.identity);
            timeCountdown = spawnTimeRate;
        }  
    }
}
