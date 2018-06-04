using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class zombieScript : MonoBehaviour
{
    //declare the transform of our goal (where the navmesh agent will move towards) and our navmesh agent (in this case our zombie)
    private Transform goal;
    private NavMeshAgent agent;
    private Animator anim;
    public GameObject zombie;
    private AudioSource audioSource;
    public AudioClip[] zombieSounds;
    private AudioClip shootClip;
    public GameObject blood;

    void Start()
    {
        goal = Camera.main.transform;
        agent = GetComponent<NavMeshAgent>();
        //set the navmesh agent's desination equal to the main camera's position (our first person character)
        agent.destination = goal.position;
        agent.speed = 0.1f + (GameManager.score/20);
        anim = GetComponent<Animator>();
        anim.SetBool("isWalking", true);

        audioSource = gameObject.GetComponent<AudioSource>();
        int index = Random.Range(0, zombieSounds.Length);
        shootClip = zombieSounds[index];
        audioSource.clip = shootClip;
        audioSource.Play();
    }


    //for this to work both need colliders, one must have rigid body, and the zombie must have is trigger checked.
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Projectile"))
        {
            //first disable the zombie's collider so multiple collisions cannot occur
            GetComponent<CapsuleCollider>().enabled = false;
            GameManager.score++;
            //destroy the bullet
            Destroy(col.gameObject);
            //stop the zombie from moving forward by setting its destination to it's current position
            agent.destination = gameObject.transform.position;
            //stop the walking animation and play the falling back animation
            anim.SetBool("isFalling", true);
            Vector3 bloodPosition = new Vector3(gameObject.transform.position.x, 2.8f, gameObject.transform.position.z);
            Instantiate(blood, bloodPosition, transform.rotation);
            //destroy this zombie in six seconds.
            Destroy(gameObject, 6);

            //set the coordinates for a new vector 3
            float randomX = UnityEngine.Random.Range(-4f, 4f);
            float constantY = .6f;
            float randomZ = UnityEngine.Random.Range(-13f, 13f);
            //set the zombies position equal to these new coordinates
            Vector3 newPosition = new Vector3(randomX, constantY, randomZ);

            //if the zombie gets positioned less than or equal to 3 scene units away from the camera we won't be able to shoot it
            //so keep repositioning the zombie until it is greater than 3 scene units away. 
            while (Vector3.Distance(newPosition, Camera.main.transform.position) <= 3)
            {

                randomX = UnityEngine.Random.Range(-12f, 12f);
                randomZ = UnityEngine.Random.Range(-13f, 13f);

                newPosition = new Vector3(randomX, constantY, randomZ);
            }

            //instantiate a new zombie

            GameObject instantiatedObj = (GameObject)Instantiate(zombie, newPosition, transform.rotation);
            instantiatedObj.name = "Zombie " + randomX;
            instantiatedObj.GetComponent<CapsuleCollider>().enabled = true;

            if (GameManager.score > 25)
            {
                GameObject instantiatedObj2 = (GameObject)Instantiate(zombie, newPosition, transform.rotation);
                instantiatedObj2.name = "Zombie " + randomX;
                instantiatedObj2.GetComponent<CapsuleCollider>().enabled = true;
            }

        }
        if (col.CompareTag("Player"))
        {
            anim.SetBool("isAttacking", true);
            col.gameObject.GetComponent<hitScript>().TakeDamage(25);
        }
    }

}
