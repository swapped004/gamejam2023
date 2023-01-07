using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class WolfAI : MonoBehaviour
{
    public Transform target;
    public float speed = 300f;
    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    public Animator animator;
    public Transform enemyGFX;

    public playerScript ps;
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();

        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<playerScript>();

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        Vector2 velocity = rb.velocity;

        velocity.x = force.x;
        rb.velocity = velocity;

        animator.SetFloat("Speed", Mathf.Abs(force.x));

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (Vector2.Distance(rb.position, target.position) < 0.5f)
        {
            animator.SetBool("Attack", true);
            ps.takeDamage(10);
        }
        else
        {
            animator.SetBool("Attack", false);
        }

        if (rb.velocity.x >= 0.01f)
        {
            enemyGFX.localScale = new Vector3(0.09662656f, 0.09152473f, 1f);
        }
        if (rb.velocity.x <= -0.01f)
        {
            enemyGFX.localScale = new Vector3(-0.09662656f, 0.09152473f, 1f);
        }
    }
}
