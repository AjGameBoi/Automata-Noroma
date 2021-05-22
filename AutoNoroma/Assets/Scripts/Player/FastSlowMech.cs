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
    public float currNoroma;
    public float maxNoromaB;
    public float maxNoromaR;
    public float NoromaBPenalty;
    public float NoromaRPenalty;
    public float NoromaStart = 0;
    public float noromaDecay;

    public int fastDamageMod;
    public int slowDamageMod;

    public GameObject NoromaB;
    public GameObject NoromaR;
    public GameObject NoromaGauge;
    public GameObject bluePowerUp;
    public GameObject redPowerUp;

    public Transform noromaBarB;
    public Transform noromaBarR;

    // Start is called before the first frame update
    void Start()
    {
        noromaBarB = NoromaB.GetComponent<Transform>();
        noromaBarR = NoromaR.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Fast();
        Slow();
        Gauge();
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
                if (bullet.GetComponent<bulletScript>().speed <= 1f)
                {
                    bullet.GetComponent<bulletScript>().speed = 1f;
                }
                bullet.GetComponent<bulletScript>().damage += slowDamageMod;
                bullet.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
    }

    void Gauge()
    {
        if ( currNoroma > 0) // Blue Decay
        {
            noromaBarB.localScale = new Vector2(currNoroma, 1);
            currNoroma -= noromaDecay * Time.deltaTime;
        }
        else
        {
            noromaBarR.localScale = new Vector2(-currNoroma, 1);
            currNoroma += noromaDecay * Time.deltaTime;
        }

        if(currNoroma > maxNoromaB)
        {
            Debug.Log("DamagedYourselfBNoroma");
        }

        if(currNoroma < maxNoromaR)
        {
            Debug.Log("DamagedYourselfRNoroma");
            //damage yourself
        }

        if(Input.GetKeyDown(KeyCode.Space)) // Noroma Blue
        {
            NoromaGauge.GetComponent<Animator>().SetTrigger("NoromaUsed");
            currNoroma += NoromaBPenalty;
        }

        if (Input.GetKeyDown(KeyCode.V)) // Noroma Red
        {
            NoromaGauge.GetComponent<Animator>().SetTrigger("NoromaUsed");
            currNoroma -= NoromaRPenalty;
        }
    }
}
