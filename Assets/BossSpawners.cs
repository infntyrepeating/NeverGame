using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BossSpawners : MonoBehaviour
{

    public GameObject[] spawners;

    public GameObject bullet;

    public static bool boss = false;


    
    private void Start()
    {
            boss = false;
            InvokeRepeating("LaunchProjectile", 5.0f, 3.0f);
            Debug.Log("Started!");
    }

    private void Update()
    {
        if (transform.position.y < -5f)
        {
            Destroy(gameObject);
        }
    }

    private void LaunchProjectile()
    {
        if (boss == true)
        {
            List<int> numbers = new List<int>()
            {
                0, 1, 2, 3, 4, 5, 6, 7, 8
            };
            
            Random r = new Random();

            int n = numbers[r.Next(0, numbers.Count)];
            Instantiate(this.bullet, spawners[n].transform.position, Quaternion.identity);
            numbers.Remove(n);

            n = numbers[r.Next(0, numbers.Count)];
            Instantiate(this.bullet, spawners[n].transform.position, Quaternion.identity);
            numbers.Remove(n);

            n = numbers[r.Next(0, numbers.Count)];
            Instantiate(this.bullet, spawners[n].transform.position, Quaternion.identity);
        }
    }
}
