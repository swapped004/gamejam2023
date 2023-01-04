using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] private float movement_speed = 2;
    [SerializeField] private float jump_power = 5;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb_player;
    private BoxCollider2D boxCollider;
    private Animator anim;

    private void Start()
    {
        
    }

    private void Awake()
    {
        rb_player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        groundLayer = LayerMask.GetMask("ground");
    }

    // Update is called once per frame
    private void Update()
    {
        float x = Input.GetAxis("Horizontal");

        rb_player.velocity = new Vector2(x * movement_speed, rb_player.velocity.y);
        
        if(x > 0.01f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        else if(x < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }


        if(Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            jump();
        }


        anim.SetBool("run", x != 0);

        anim.SetBool("jump", !isGrounded());
        
    }


    private void jump()
    {
        rb_player.velocity = new Vector2(rb_player.velocity.x, jump_power);
        anim.SetTrigger("jump_trigger");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if(collision.gameObject.tag == "ground")
        // {
        //     grounded = true;
        // }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, groundLayer);
        return raycastHit.collider != null;
    }
}
