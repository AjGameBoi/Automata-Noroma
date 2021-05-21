using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeCharCont : MonoBehaviour
{
    Rigidbody2D rigidBody;
    public float speed;
    public float jump;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.D))
        {
            rigidBody.AddForce(new Vector2(speed, 0), ForceMode2D.Force);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rigidBody.AddForce(new Vector2(-speed, 0), ForceMode2D.Force);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            rigidBody.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
        }
        /*if (Input.GetKeyDown(KeyCode.D))
        {
            rigidBody.AddForce(new Vector2(0, -speed));
        }*/
    }
}
