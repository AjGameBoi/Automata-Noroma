using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCript : MonoBehaviour
{
    public GameObject homingBullet;
    public GameObject normalBullet;
    public GameObject[] spawnPoints;
    public GameObject eyeSpawn;

    Animator anim;

    public int eyeAttackTimes = 15;

    public float currentCoolDownTime;
    public float cooldownRate;
    public float minCooldown = 5f;
    public float maxCooldown = 10f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        cooldownRate = Random.Range(minCooldown, maxCooldown);
        currentCoolDownTime = cooldownRate;
    }

    // Update is called once per frame
    void Update()
    {
        ChooseAttack(); ;
    }

    void ChooseAttack()
    {
        currentCoolDownTime -= Time.deltaTime;
        if(currentCoolDownTime < 0)
        {
            int randomNumber = Random.Range(1, 3);
            anim.SetTrigger("Attack" + randomNumber);
            currentCoolDownTime = cooldownRate;
        }
    }

    public void AttackTwo()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(homingBullet, spawnPoints[i].transform.position,
                Quaternion.identity);
        }
    }

    public void EyeAttack()
    {
        for (int i = 0; i < eyeAttackTimes; i++)
        {
                Instantiate(normalBullet, eyeSpawn.transform.position, Quaternion.identity);
                currentCoolDownTime = cooldownRate;
        }

    }
}
