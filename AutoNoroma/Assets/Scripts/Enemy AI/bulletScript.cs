using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public float lifetime = 3f;
    public float speed;
    public float stepRotation = 10f;

    public int damage = 1;

    Transform player;
    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector3(player.position.x, player.position.y, 0);

        //transform.rotation = Quaternion.RotateTowards(transform.rotation, player.rotation, stepRotation);
        transform.right = target - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;

        lifetime -= Time.deltaTime;
        if(lifetime < 0)
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
    }
}
