using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour
{

    private GameObject gun;
    private GameObject spawnPoint;
    public GameObject bullet;

    void Start()
    {
        //create references to gun and bullet spawnPoint objects
        spawnPoint = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //declare a new RayCastHit
        RaycastHit hit;
        //draw the ray for debuging purposes (will only show up in scene view)
        Debug.DrawRay(spawnPoint.transform.position, spawnPoint.transform.forward, Color.green);
        //cast a ray from the spawnpoint in the direction of its forward vector
        if (Physics.Raycast(spawnPoint.transform.position, spawnPoint.transform.forward, out hit, 100))
        {
            //if the raycast hits any game object where its name contains "zombie" and we aren't already shooting we will start the shooting coroutine
            if (hit.collider.name.Contains("Zombie") && GameManager.currentHealth > 0)
            {
                //instantiate the bullet
                GameObject instantiatedObj = (GameObject)Instantiate(bullet, spawnPoint.transform.position, spawnPoint.transform.rotation);
                    //Get the bullet's rigid body component and set its position and rotation equal to that of the spawnPoint
                    Rigidbody rb = instantiatedObj.GetComponent<Rigidbody>();
                    //add force to the bullet in the direction of the spawnPoint's forward vector
                    rb.AddForce(spawnPoint.transform.forward * 5000f);
                    //play the gun shot sound and gun animation
                    GetComponent<AudioSource>().Play();
                    //destroy the bullet after 1 second
                    Destroy(instantiatedObj, 1);
                }

            }


    }
        
    }
