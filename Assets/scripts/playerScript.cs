using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerScript : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] private float movement_speed = 2;
    [SerializeField] private float jump_power = 5;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [SerializeField] private SpriteRenderer sprite;

    [SerializeField] private Rigidbody2D rb_player;
    [SerializeField] private BoxCollider2D boxCollider;
    private Animator anim;
    private bool isAlive = true;
    private int score = 0;
    private bool powered_up = false;


    public portalControllerScript portalController;
    private float apple_multiplier = 1.4f;

    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthbar;

    public GameObject missingMessage;
    public AudioSource pickSound;
    public AudioSource jumpSound;
    public AudioSource hurtSound;
    public AudioSource damageSound;
    private void Start()
    {
        portalController = GameObject.FindGameObjectWithTag("portalController").GetComponent<portalControllerScript>();

        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }

    private void Awake()
    {
        rb_player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        groundLayer = LayerMask.GetMask("ground");
        wallLayer = LayerMask.GetMask("wall");

    }

    // Update is called once per frame
    private void Update()
    {
        float x = Input.GetAxis("Horizontal");

        if(isAlive)
            rb_player.velocity = new Vector2(x * movement_speed, rb_player.velocity.y);
        
        //get the center of the box collider
        Vector3 center = boxCollider.bounds.center;

        //flip the player
        if(x > 0f && isAlive)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
            
        }

        else if(x < 0f && isAlive)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        }


        if(Input.GetKeyDown(KeyCode.Space) && isGrounded() && isAlive)
        {
            jump();
        }


        anim.SetBool("run", x != 0);
        anim.SetBool("jump", !isGrounded());

        if(Input.GetKeyDown(KeyCode.E) && isAlive)
        {
            portalController.spawnPortal();
        }


        


    }


    private void jump()
    {
        playSound(jumpSound);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "portal")
        {
            portalController.teleport();
        }

        else if(collision.gameObject.tag == "apple")
        {
            //get the apple game object
            playSound(pickSound);
            // pickSound.Play();
            GameObject apple = collision.gameObject;

            

            if(!powered_up)
            {
                transform.localScale = new Vector3(apple_multiplier * transform.localScale.x , apple_multiplier * transform.localScale.y, transform.localScale.z);
                jump_power = apple_multiplier * jump_power;
            }

            powered_up = true;
            
            score++;
            Destroy(apple);
            Destroy(missingMessage);
        }

        else if(collision.gameObject.tag == "cherry")
        {
            //get the apple game object
            playSound(pickSound);
        }
        
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down*transform.localScale.y, .1f, groundLayer);
        return raycastHit.collider != null;
    }


    private bool isTouchingWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), .1f, wallLayer);
        return raycastHit.collider != null;
    }


    public void flipGravity()
    {
        rb_player.gravityScale *= -1;
        transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, 1);
    }

    public void takeDamage(int damage)
    {
        playSound(damageSound);
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);

        if(currentHealth <= 0)
        {   
            Debug.Log("player is dead");
            isAlive = false;
            this.kill_player();
        }

    }
    public void kill_player()
    {
        playSound(hurtSound);
        isAlive = false;
        currentHealth = maxHealth;
        anim.SetTrigger("death");
    }

    public void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void getNextScene()
    {
        if(SceneManager.GetActiveScene().buildIndex >= 5)
            SceneManager.LoadScene(2);
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public int getScore()
    {
        return score;
    }


    public void playSound(AudioSource clip)
    {
        clip.Play();
    }
}
