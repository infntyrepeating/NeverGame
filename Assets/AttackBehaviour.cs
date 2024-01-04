using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    public string excludedTag = "Player";

    public string playerTag = "Player"; // Assign the reference GameObject in the Inspector
    public float destroyDistance = 2f;
    public float maxScale = 3f; // Maximum scale
    public float scaleSpeed = 3f; // Speed of scale change


    // Start is called before the first frame update
    void Start()
    {
        // Find all GameObjects with the specified tag
        GameObject[] excludedObjects = GameObject.FindGameObjectsWithTag(excludedTag);

        // Ignore collisions between the current GameObject and all GameObjects with the specified tag
        foreach (GameObject excludedObject in excludedObjects)
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), excludedObject.GetComponent<Collider2D>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject playerObject = GameObject.FindWithTag(playerTag);
        float distance = Vector3.Distance(transform.position, playerObject.transform.position);

            // Check if the distance exceeds the destroyDistance
        if (distance > destroyDistance)
        {
                // Self-destruct (destroy the GameObject)
            Destroy(gameObject);
        }

        float newScale = Mathf.Lerp(transform.localScale.x, Mathf.Clamp(10f / maxScale, 1f, maxScale), scaleSpeed * Time.deltaTime);
        transform.localScale = new Vector3(newScale, newScale, 1f);
    }
}
