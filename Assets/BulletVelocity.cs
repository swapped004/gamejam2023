using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletVelocity : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 1.5f;
    public Rigidbody2D rb;
    public enemyScript enemy;
    void Start()
    {
        GameObject player  = GameObject.FindGameObjectWithTag("Player");
        rb.velocity = transform.right * player.transform.localScale.x* speed;
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<enemyScript>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy")
        {
            enemy.TakeDamage(20);
            //Destroy(gameObject);
        }
        if (!(other.gameObject.tag == "Player")) {
            Destroy(gameObject);
        }
    }
}
