using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCript : MonoBehaviour
{
    public GameObject prefab;
    public GameObject[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttackTwo()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject clone = (GameObject)Instantiate(prefab, spawnPoints[i].transform.position,
                Quaternion.identity);
            spawnPoints[i] = clone;
        }
    }
}
