using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private float powerUpStrength = 15.0f;
    public float speed = 5.0f;
    
    public bool hasPowerup = false;

    public GameObject powerupIndicator;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");

        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);

        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
  

    }

    private void OnTriggerEnter(Collider other)
    {
        //check to see if we collide with another game object whos tag is "Powerup"
        if(other.CompareTag("Powerup"))
        {
            //set hasPowerup=true after colliding with another object named Powerup
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    //The Ienumerator is something in programming known as an interface.
    //Helps enable a countdown timer outside of the update loop
    IEnumerator PowerupCountdownRoutine()
    {
        //enable to run this timer outside of our update loop
        //will use the wait for seconds method to wait for 7 seconds then
        //once timer has stopped. can set has power up to false. and reset player state
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {

        //check if we collide with another game object whos tag is enemy and if we have the power up.
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            enemyRigidbody.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
            Debug.Log("Collided with: " + collision.gameObject.name + " with power up set to " + hasPowerup);

        }
    }
}
