using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject apple;
    public float health;
    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }
    
    public void Die()
    {
        Destroy(gameObject);
        Instantiate(apple, transform.position, Quaternion.identity);
    }

}
