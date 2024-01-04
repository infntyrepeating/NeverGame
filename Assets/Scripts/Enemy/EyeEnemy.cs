using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeEnemy : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public GameObject targetObject;
    public float spawnRadius = 7f;
    public float spawnInterval = 3f;
    public float moveSpeed = 5f;

    void Start()
    {
        InvokeRepeating("SpawnPrefab", 0f, spawnInterval);
    }

    void SpawnPrefab()
    {
        if (IsInRadius())
        {
            // Calculate the direction from this object to the target object
            Vector3 direction = (targetObject.transform.position - transform.position).normalized;

            // Spawn the prefab in the calculated position
            GameObject spawnedPrefab = Instantiate(prefabToSpawn, transform.position + direction, Quaternion.identity);
        }
    }

    bool IsInRadius()
    {
        // Check if the distance between this object and the target object is within the spawn radius
        float distance = Vector3.Distance(transform.position, targetObject.transform.position);
        return distance <= spawnRadius;
    }
}
