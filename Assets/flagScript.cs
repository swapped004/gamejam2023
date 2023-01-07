using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class flagScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private int targetScore;
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
        //get the player score
        int playerScore = player.GetComponent<playerScript>().getScore();


        if (other.gameObject.tag == "Player" && playerScore >= targetScore)
        {
            print("Player has entered the flag zone");	
            Debug.Log("Player has entered the flag zone");

            //load next level
            print("Loading next level:"+ (SceneManager.GetActiveScene().buildIndex + 1));
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
