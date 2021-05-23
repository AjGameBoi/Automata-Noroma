using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class homingbulletscript : MonoBehaviour
{
    public FieldOfView fov;
    public float lifetime = 30f;
    public float speed;
    public float stepRotation = 10f;

    public int damage = 1;
    public bool isReflected;
    public int currentCounter;
    public int maxCounter = 2;
    public bool isHoming = true;
    public int listSize;

    Transform player;
    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
        fov = FindObjectOfType<FieldOfView>();
        listSize = fov.visibleTargets.Count;
        isReflected = false;
        currentCounter = 0;

        //transform.rotation = Quaternion.RotateTowards(transform.rotation, player.rotation, stepRotation);
        transform.right = target - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCounter >= maxCounter && listSize != 0)
        {
            isReflected = true;
            transform.position = Vector2.MoveTowards(transform.position, fov.visibleTargets[0].transform.position, speed * Time.deltaTime);
        }
        else if (listSize == 0)
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
        else if (listSize != 0)
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
        
        lifetime -= Time.deltaTime;
        if (lifetime < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            var healthComponent = collision.GetComponent<PlayerController>();

            if(healthComponent != null)
            {
                healthComponent.TakeDamage(1);
                Destroy(gameObject);
                Debug.Log("Player is Hit");
            }
        }
        
        if(collision.tag == "Enemy" && isReflected != false)
        {
            var healthComponent = collision.GetComponent<EnemyAI>();

            if(healthComponent != null)
            {
                healthComponent.TakeDamage(1);
                Destroy(gameObject);
                Debug.Log("Enemy is Hit");
            }
        }
    }
}
