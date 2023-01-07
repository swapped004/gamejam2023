using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallBreak_trigger_script : MonoBehaviour
{
    [SerializeField] private GameObject wallToBreak;
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
            Debug.Log("Player has entered the trigger zone");
            // Destroy the wall
            Destroy(this.wallToBreak);
        }
    }
}
