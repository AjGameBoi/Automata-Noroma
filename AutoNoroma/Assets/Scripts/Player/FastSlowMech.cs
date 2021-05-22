using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastSlowMech : MonoBehaviour
{
    Collider2D[] bullets;

    public LayerMask bulletLayer;

    public float radiusSize = 5f;
    public float fastSpeedMod;
    public float slowSpeedMod;

    public int fastDamageMod;
    public int slowDamageMod;

    public GameObject bluePowerUp;
    public GameObject redPowerUp;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Fast();
        Slow();
    }

    void Fast()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Fast
        {
            bluePowerUp.SetActive(false);
            bluePowerUp.SetActive(true);
            Debug.Log("Looking for bullets");
            bullets = Physics2D.OverlapCircleAll(transform.position, radiusSize, bulletLayer);

            foreach (var bullet in bullets)
            {
                Debug.Log("Modified a bullet");
                bullet.GetComponent<bulletScript>().speed += fastSpeedMod;
                bullet.GetComponent<bulletScript>().damage -= fastDamageMod;
                bullet.GetComponent<SpriteRenderer>().color = Color.blue;
            }
        }
    }

    void Slow()
    {
        if (Input.GetKeyDown(KeyCode.V)) // Fast
        {
            redPowerUp.SetActive(true);
            Debug.Log("Looking for bullets");
            bullets = Physics2D.OverlapCircleAll(transform.position, radiusSize, bulletLayer);

            foreach (var bullet in bullets)
            {
                Debug.Log("Modified a bullet");
                bullet.GetComponent<bulletScript>().speed -= slowSpeedMod;
                if(bullet.GetComponent<bulletScript>().speed <= 1f)
                {
                    bullet.GetComponent<bulletScript>().speed = 1f;
                }
                bullet.GetComponent<bulletScript>().damage += slowDamageMod;
                bullet.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
    }
}
