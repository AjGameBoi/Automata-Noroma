using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTest : MonoBehaviour
{
    //Movement and Flipping
    float dir = 1;
    public float speed;
    public float jumpHeight;
    private Rigidbody2D playerRB;
    bool facingRight = true;
    
    //Swipe Controls
    public float maxSwipeTime;
    public float minSwipeDistance;
    private float swipeStartTime;
    private float swipeEndTime;
    private float swipeTime;

    private float swipeLength;
    private Vector2 startSwipePosition;
    private Vector2 endSwipePosition;
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        playerRB.velocity = new Vector2(dir * speed * Time.deltaTime, playerRB.velocity.y);
        SwipeTest();

    }

    void FlipAndMove()
    {
        dir = -dir;
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
    }
    void SwipeTest()
    {
        if (Input.touchCount > 0)
        {
            Touch touch =Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                swipeStartTime = Time.time;
                startSwipePosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                swipeEndTime = Time.time;
                endSwipePosition = touch.position;
                swipeTime = swipeEndTime - swipeStartTime;
                swipeLength = (endSwipePosition - startSwipePosition).magnitude;

                if (swipeTime<maxSwipeTime && swipeLength > minSwipeDistance)
                {
                    SwipeControl();
                }
            }
        }
    }

    void SwipeControl()
    {
        Vector2 Distance = endSwipePosition - startSwipePosition;
        float xDistance = Mathf.Abs(Distance.x);
        float yDistance = Mathf.Abs(Distance.y);

        if (xDistance > yDistance)
        {
            if (Distance.x > 0 && !facingRight)
            {
                FlipAndMove();
            }
            else if (Distance.x < 0 && facingRight)
            {
                FlipAndMove();
            }
        }
        else if (yDistance > xDistance)
        {
            if (Distance.y > 0)
            {
                playerRB.velocity = Vector2.up * jumpHeight * Time.deltaTime;
            }
            else if (Distance.y < 0)
            {
                Debug.Log ("Going Down");
            }
        }
    }
}
