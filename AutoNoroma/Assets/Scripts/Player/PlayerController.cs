using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Text outputText;
    private Rigidbody2D playerRB;
    public float speed;
    public float slowSpeed;
    public float jumpForce;
    private Vector2 movement;
    private int playerLayer, platformLayer;
    private bool jumpAllowed = false;
    private bool jumpOffCouroutineIsRunning = false;
    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    private Vector2 endTouchPosition;

    private bool stopTouch = false;

    public float swipeRange;
    public float tapRange;
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerLayer = LayerMask.NameToLayer("Player");
        platformLayer = LayerMask.NameToLayer("Platform");
    }

    void Update()
    {
        Swipe();
    }

    private void FixedUpdate() 
    {
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

            if (!stopTouch)
            {
                if (Distance.x < -swipeRange)
                {
                    movement = new Vector2(-1, 0);
                    outputText.text = "Left";
                    stopTouch = true;
                }
                else if (Distance.x > swipeRange)
                {
                    movement = new Vector2(1, 0);
                    outputText.text = "Right";
                    stopTouch = true;
                }
                else if (Distance.y > swipeRange && playerRB.velocity.y == 0)
                {
                    jumpAllowed = true;
                    outputText.text = "Jump";
                    stopTouch = true;
                }
                else if (Distance.y < -swipeRange)
                {
                    StartCoroutine ("JumpOff");
                    outputText.text = "Down";
                    stopTouch = true;
                }
            }

            if (playerRB.velocity.y > 0)
            {
                Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, true);
            }
            else if (playerRB.velocity.y <= 0 && !jumpOffCouroutineIsRunning)
            {
                Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, false);
            }
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            stopTouch = false;
            endTouchPosition = Input.GetTouch(0).position;
            Vector2 Distance = endTouchPosition - startTouchPosition;

            if (Mathf.Abs(Distance.x) < tapRange && Mathf.Abs(Distance.y) < tapRange )
            {
                movement = new Vector2(slowSpeed *Mathf.Sign(playerRB.velocity.x), playerRB.velocity.y);
                outputText.text = "Tap";
            }
        }
    }

    void MoveCharacter(Vector2 direction)
    {
        playerRB.AddForce(direction * speed);
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

}
