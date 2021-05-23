using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryScript : MonoBehaviour
{
    private float curTime = 5f;
    void Start()
    {
        
    }

    void Update()
    {
        curTime -= Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Bullet")
        {
            Debug.Log("Parry");
            //other.GetComponent<bulletScript>();
            other.GetComponent<Rigidbody2D>().isKinematic = false;
            other.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            other.GetComponent<Rigidbody2D>().angularVelocity = 0;

            if (curTime <= 0.0001f)
                {
                    other.GetComponent<Rigidbody2D>().AddForce(new Vector2(3,4), ForceMode2D.Impulse);
                }
        }
    }
}
