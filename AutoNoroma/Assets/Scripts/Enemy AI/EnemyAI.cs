
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float jumpForce;
    public float walkSpeed;
    public float cooldownRate = 2.0f;
    [SerializeField]float currentCoolDownTime;

    Transform player;

    public LayerMask groundLayer;

    Rigidbody2D rigidBody;

    public Collider2D bodyCollider;

    public GameObject bullet;

    [HideInInspector] bool mustPatrol;
    bool mustFlip;

    int jumpOrNo;

    Transform groundCheckPos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        jumpOrNo = Random.Range(0, 100);
        rigidBody = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<BoxCollider2D>();
        groundCheckPos = GetComponentInChildren<Transform>();
        mustPatrol = true;

        currentCoolDownTime = cooldownRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (mustPatrol)
        {
            Patrol();
        }
        Attack();
    }

    private void FixedUpdate()
    {
        if (mustPatrol)
        {
            mustFlip = Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        }
    }

    void Patrol()
    {
        if(mustFlip || bodyCollider.IsTouchingLayers(groundLayer))
        {
            Debug.Log("Unsuccessful");
            jumpOrNo = Random.Range(0, 100);
            Flip();
        }
        /*else if(mustFlip && jumpOrNo > 50)
        {
            Debug.Log("success");
            rigidBody.AddForce(new Vector2(0, jumpForce * Time.deltaTime), ForceMode2D.Impulse);
            Debug.Log("Randomized");
            jumpOrNo = Random.Range(0, 100);
        }*/
        rigidBody.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rigidBody.velocity.y);
    }

    void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustPatrol = true;
    }

    void Attack()
    {
        currentCoolDownTime -= Time.deltaTime;
        if (currentCoolDownTime < 0)
        {
            Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z));
            currentCoolDownTime = cooldownRate;
        }
    }
}
