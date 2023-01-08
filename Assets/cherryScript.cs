using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cherryScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public AudioSource cherrySound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //get the player score
            player.GetComponent<playerScript>().playSound(cherrySound);
           
            Debug.Log("Player has entered the cherry zone");
            
            // Flip gravity
            player.GetComponent<playerScript>().flipGravity();
             // Destroy the cherry
            Destroy(this.gameObject);
        }
    }
}
