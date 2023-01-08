using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletVelocity : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 1.5f;
    public Rigidbody2D rb;
    public enemyScript enemy;

    public BossHealth bh;
    void Start()
    {
        GameObject player  = GameObject.FindGameObjectWithTag("Player");
        rb.velocity = transform.right * player.transform.localScale.x* speed;
        if(GameObject.FindGameObjectWithTag("Enemy"))
            enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<enemyScript>();
        if(GameObject.FindGameObjectWithTag("Boss"))
            bh = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossHealth>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy")
        {
            enemy.TakeDamage(10);
            //Destroy(gameObject);
        }
        if (other.gameObject.tag == "Boss")
        {   
            bh.TakeDamage(10);
            //Destroy(gameObject);
        }
        if (!(other.gameObject.tag == "Player")) {
            Destroy(gameObject);
        }
    }
}
