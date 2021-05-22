using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] spawnee;
    public bool stopSpawning = false;
    public float spawnTime = 0.5f;
    public float minTime = 1.5f;
    public float maxTime = 5f;
    int objectIndex;

    public int maxEnemies;

    // Start is called before the first frame update
    void Start()
    {
        //RandomSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        /*var enemyCount : int = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemyCount < maxEnemies)
        {
            Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        }*/
    }
        
    void RandomSpawn()
    {
        float randomTime = Random.Range(minTime, maxTime);

        objectIndex = Random.Range(0, spawnee.Length);

        Instantiate(spawnee[objectIndex], transform.position, transform.rotation);

        Invoke("RandomSpawn", randomTime);
    }
}
