using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalControllerScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject portal_1;
    [SerializeField] private GameObject portal_2;

   
    [SerializeField] private float portal_offset_from_player = 0.2f;
    [SerializeField] private float minimum_distance_between_portals = 2f;


    private int number_of_portals = 0;
    private Vector3 portal_1_position;
    private Vector3 portal_2_position;
    private GameObject portal_1_instance;
    private GameObject portal_2_instance;
    public AudioSource portalSound;
    public AudioSource portalSound2;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    public void spawnPortal()
    {
        if(number_of_portals == 0)
        {
            Vector3 portal_position = new Vector3(player.transform.position.x + portal_offset_from_player*player.transform.localScale.x, player.transform.position.y, player.transform.position.z);
            portal_1_instance = Instantiate(portal_1, portal_position, Quaternion.identity);
            portal_1_position = portal_position;
            number_of_portals++;
            portalSound.Play();
        }

        else if(number_of_portals == 1)
        {
            Vector3 portal_position = new Vector3(player.transform.position.x + portal_offset_from_player*player.transform.localScale.x, player.transform.position.y, player.transform.position.z);
            //check if the distance between the two portals is greater than the minimum distance
            if(Vector3.Distance(portal_position, portal_1.transform.position) > minimum_distance_between_portals)
            {
                portal_2_instance = Instantiate(portal_2, portal_position, Quaternion.identity);
                portal_2_position = portal_position;
                number_of_portals++;
                portalSound.Play();
            }

            else
            {
                Debug.Log("The distance between the two portals is less than the minimum distance");
            }
        }

        else 
        {
           Debug.Log("You have already spawned two portals");
        }
    }


    public void teleport()
    {
        if(number_of_portals == 2)
        {
            print("teleport");

            //check which portal is closer to the player
            
           portalSound2.Play();
            if(Vector3.Distance(player.transform.position, portal_1_position) < Vector3.Distance(player.transform.position, portal_2_position))
            {
                player.transform.position = new Vector3(portal_2_position.x + portal_offset_from_player*player.transform.localScale.x, portal_2_position.y, portal_1_position.z);
            }

            else
            {
                player.transform.position = new Vector3(portal_1_position.x + portal_offset_from_player*player.transform.localScale.x, portal_1_position.y, portal_1_position.z);
            }

            number_of_portals = 0;
            //destroy the portals
            //get the portal scripts
            portalScript portal_1_script = portal_1_instance.GetComponent<portalScript>();
            portalScript portal_2_script = portal_2_instance.GetComponent<portalScript>();

            portal_1_script.delete_portal();
            portal_2_script.delete_portal();
        }

    }
}
