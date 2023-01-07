using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatingPlatformScript : MonoBehaviour
{
    [SerializeField] private float movement_speed = 0.05f;
    [SerializeField] private float offset = 1;
    [SerializeField] private int direction = 1;

    private float left_max;
    private float right_max;
    
    // Start is called before the first frame update
    void Start()
    {
        left_max  = transform.position.x - offset;
        right_max = transform.position.x + offset;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < left_max)
        {
            direction = 1;
        }
        if(transform.position.x > right_max)
        {
            direction = -1;
        }

        transform.Translate(Vector2.right * movement_speed * direction * Time.deltaTime);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.transform.parent = transform;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.transform.parent = null;
        }
    }
}
