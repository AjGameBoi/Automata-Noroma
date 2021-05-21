using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class homingbulletscript : MonoBehaviour
{
    public float lifetime = 3f;
    public float speed;
    public float stepRotation = 10f;

    public int damage = 1;

    Transform player;
    Vector2 target;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);

        //transform.rotation = Quaternion.RotateTowards(transform.rotation, player.rotation, stepRotation);
        transform.LookAt(target);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        lifetime -= Time.deltaTime;
        if (lifetime < 0)
        {
            Destroy(gameObject);
        }
    }

    void DamagePlayer(int damage)
    {
        //Playerscript Health, -= damage
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //hit player, call DamagePlayer()
    }
}
