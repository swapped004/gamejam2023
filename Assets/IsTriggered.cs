using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTriggered : MonoBehaviour
{
     public GameObject target;

     private void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.tag == "Player") {
                 target.SetActive(true);
            }
     }
}
