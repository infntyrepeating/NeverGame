using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject target;
    public float moveSpeed = 5f;
    Vector3 direction;
    Rigidbody2D rb;

    void Start()
    {

        target = GameObject.Find("Player");
            // Calculate the direction from this object to the target object
            direction = (target.transform.position - transform.position).normalized;

            // Get the Rigidbody component of this object
            rb = GetComponent<Rigidbody2D>();

            // Ensure there is a Rigidbody component attached
            if (rb != null)
            {
                // Set the velocity to move in the calculated direction
                rb.velocity = direction * moveSpeed;
            }
            else
            {
                Debug.LogError("Rigidbody component is missing!");
            }
    }

    void Update()
    {
        rb.velocity = direction * moveSpeed;
    }

    // This method is called when the Collider enters the trigger zone
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Barrier"))
        {
            Destroy(gameObject);
            Debug.Log("Entered Trigger #1 Zone!");
        }// Check if the entering collider is the one you are interested in
        else if (other.CompareTag("Player")) // Change "Player" to the tag of the object you want to trigger
        {
            Destroy(gameObject);
            // Set the boolean variable to true
            PlayerMovement.life--;
            if (PlayerMovement.life == 0) { PlayerMovement.Alive = false; }

            // You can add more actions or logic here if needed
            Debug.Log("Entered Trigger Zone!");
        }
    }

}
