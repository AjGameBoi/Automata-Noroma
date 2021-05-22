using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //public Text outputText;
    //Player Movement
    private Rigidbody2D playerRB;
    public Animator animator;
    public Animator imageAnimator;
    [SerializeField] private Transform groundCheckCollider;
    [SerializeField] LayerMask groundLayer;
    const float groundCheckRadius = 0.2f;
    [SerializeField] public float speed;
    public float slowSpeed;
    public float jumpForce;
    private Vector2 movement;
    private int playerLayer, platformLayer;
    private bool jumpAllowed = false;
    private bool jumpOffCouroutineIsRunning = false;

    //Player Health
    public int maxHealth = 3;
    public int currentHealth;

    //Delay After Death
    private float delayBeforeLoading = 1.4f;
    private float timeElapsed;
    [SerializeField] private bool isDead;
    
    //Mobile Controls
    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    private Vector2 endTouchPosition;

    private bool stopTouch = false;
    [SerializeField] private bool isGrounded = false;
    public float swipeRange;
    public float tapRange;
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerLayer = LayerMask.NameToLayer("Player");
        platformLayer = LayerMask.NameToLayer("Platform");
        currentHealth = maxHealth;
    }

    void Update()
    {
        if(isDead == true)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed > delayBeforeLoading)
            {
                SceneManager.LoadScene("Game Over");
            }
        }
        Swipe();
    }

    private void FixedUpdate() 
    {
        GroundCheck();
        MoveCharacter(movement);
        JumpIfAllowed();
    }

    public void Swipe()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            currentTouchPosition = Input.GetTouch(0).position;
            Vector2 Distance = currentTouchPosition - startTouchPosition;

            Vector2 characterScale = transform.localScale;
            if (!stopTouch)
            {
                if (Distance.x < -swipeRange)
                {
                    characterScale.x = -3.2f;
                    movement = new Vector2(-1, 0);
                    animator.SetFloat("Speed",1);
                    //outputText.text = "Left";
                    stopTouch = true;
                }
                else if (Distance.x > swipeRange)
                {
                    characterScale.x = 3.2f;
                    movement = new Vector2(1, 0);
                    animator.SetFloat("Speed",1);
                    //outputText.text = "Right";
                    stopTouch = true;
                }
                else if (Distance.y > swipeRange && playerRB.velocity.y == 0)
                {
                    jumpAllowed = true;
                    animator.SetBool("isJumping" , true);
                    //outputText.text = "Jump";
                    stopTouch = true;
                }
                else if (Distance.y < -swipeRange)
                {
                    StartCoroutine ("JumpOff");
                    //outputText.text = "Down";
                    stopTouch = true;
                }
            }
            transform.localScale = characterScale;

            if (playerRB.velocity.y > 0)
            {
                Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, true);
            }
            else if (playerRB.velocity.y <= 0 && !jumpOffCouroutineIsRunning)
            {
                Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, false);
            }
        }

        animator.SetFloat("ySpeed", playerRB.velocity.y);

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            stopTouch = false;
            endTouchPosition = Input.GetTouch(0).position;
            Vector2 Distance = endTouchPosition - startTouchPosition;

            if (Mathf.Abs(Distance.x) < tapRange && Mathf.Abs(Distance.y) < tapRange )
            {
                movement = new Vector2(slowSpeed *Mathf.Sign(playerRB.velocity.x), 0.5f);
                animator.SetFloat("Speed",0);
                //outputText.text = "Tap";
            }
        }
    }

    void MoveCharacter(Vector2 direction)
    {
        playerRB.AddForce(direction * speed);
    }
    void GroundCheck()
    {
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0)
        {
            isGrounded = true;
        }
        Collider2D[] platformcolliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, platformLayer);
        if (platformcolliders.Length > 0)
        {
            isGrounded = true;
        }

        // If "grounded", the jump bool is disabled
        animator.SetBool("isJumping", !isGrounded);
    }
    IEnumerator JumpOff()
    {
        jumpOffCouroutineIsRunning = true;
        Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, true);
        yield return new WaitForSeconds (0.5f);
        Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, false);
        jumpOffCouroutineIsRunning = false;
    }
    private void JumpIfAllowed()
    {
        if (jumpAllowed)
        {
            playerRB.AddForce (Vector2.up * jumpForce);
            jumpAllowed = false;
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth == 3)
        {
            imageAnimator.SetFloat("Health", 3);
        }
        else if (currentHealth == 2)
        {
            imageAnimator.SetFloat("Health", 2);
        }
        else if (currentHealth == 1)
        {
            imageAnimator.SetFloat("Health", 1);
        }
        else if (currentHealth == 0)
        {
            animator.SetBool("isDead", true);
            playerRB.constraints = RigidbodyConstraints2D.FreezeAll;
            isDead = true;
            imageAnimator.SetFloat("Health", 0);
        }
    }

}
