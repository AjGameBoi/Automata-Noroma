using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [HideInInspector] bool mustPatrol;
    public float jumpForce;
    public LayerMask groundLayer;
    public float walkSpeed;
    Rigidbody2D rigidbody;
    bool mustFlip;
    public Collider2D bodyCollider;
    int jumpOrNo;

    Transform groundCheckPos;

    // Start is called before the first frame update
    void Start()
    {
        jumpOrNo = Random.Range(0, 100);
        rigidbody = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<BoxCollider2D>();
        groundCheckPos = GetComponentInChildren<Transform>();
        mustPatrol = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(mustPatrol)
        {
            Debug.Log("Randomized");
            jumpOrNo = Random.Range(0, 100);
            Patrol();
        }
    }

    private void FixedUpdate()
    {
        if(mustPatrol)
        {
            mustFlip = Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        }
    }

    void Patrol()
    {
        if(mustFlip || bodyCollider.IsTouchingLayers(groundLayer))
        {
            Debug.Log("Unsuccessful");
            Flip();
        }
        else if(mustFlip && jumpOrNo > 50)
        {
            Debug.Log("success");
            rigidbody.AddForce(new Vector2(0, jumpForce * Time.deltaTime), ForceMode2D.Impulse);
        }
        rigidbody.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rigidbody.velocity.y);
    }

    void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustPatrol = true;
    }
}
