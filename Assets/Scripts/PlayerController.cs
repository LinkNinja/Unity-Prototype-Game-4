using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public float speed = 5.0f;

    public bool hasPowerup = false;
    
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
  

    }

    private void OnTriggerEnter(Collider other)
    {
        //check to see if we collide with another game object whos tag is "Powerup"
        if(other.CompareTag("Powerup"))
        {
            //set hasPowerup=true after colliding with another object named Powerup
            hasPowerup = true;
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        //check if we collid with another game object whos tag is enemy and if we have the power up.
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Debug.Log("Collided with: " + collision.gameObject.name + " with power up set to " + hasPowerup);
        }
    }
}
