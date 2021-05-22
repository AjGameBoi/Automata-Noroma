using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCountLimiter : MonoBehaviour
{
    public GameObject enemies;

    bool enemiesMaxReached;

    public int maxEnemies = 5;

    public GameObject[] spawnee;
    public Transform[] spawnPoints;
    public bool stopSpawning = false;
    public float spawnTime = 0.5f;
    public float minTime = 1.5f;
    public float maxTime = 5f;
    int objectIndex;
    int spawnIndex;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < maxEnemies; i++)
        {
            RandomSpawn();
        }
        /*
        if (enemies.Length >= maxEnemies)
        {
            var enemiesNeeded = maxEnemies - enemies.Length;

        }*/
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if(enemies.Length >= maxEnemies)
        {
            Destroy(GameObject.FindGameObjectWithTag("Enemy").gameObject);
        }
    }

    void RandomSpawn()
    {
        Debug.Log("RandomSpawnFunction");
        float randomTime = Random.Range(minTime, maxTime);

        objectIndex = Random.Range(0, spawnee.Length);
        spawnIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(spawnee[objectIndex], spawnPoints[spawnIndex].position, transform.rotation);

        Invoke("RandomSpawn", randomTime);
    }
}
