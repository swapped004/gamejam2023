using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb_player;
    public Animator anim;
    public float horizontal_speed = 2;
    public float vertical_speed = 5;

    public bool grounded;

    void Start()
    {
        
    }

    public void Awake()
    {
        rb_player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");

        rb_player.velocity = new Vector2(x * horizontal_speed, rb_player.velocity.y);
        
        if(x > 0.01f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        else if(x < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }


        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            jump();
        }


        anim.SetBool("run", x != 0);

        anim.SetBool("jump", !grounded);
        
    }


    public void jump()
    {
        rb_player.velocity = new Vector2(rb_player.velocity.x, vertical_speed);
        anim.SetTrigger("jump_trigger");
        grounded = false;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            grounded = true;
        }
    }
}
