using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmHeat : MonoBehaviour
{
    public float currentTime;
    public float timeMax = 3;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = timeMax;
        animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            animator.SetBool("Heating", true);
            currentTime -= Time.deltaTime;
            if(currentTime < 0)
            {
                collision.GetComponent<PlayerController>().currentHealth -= 1;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animator.SetBool("Heating", false);
            currentTime = timeMax;
        }
    }
}
