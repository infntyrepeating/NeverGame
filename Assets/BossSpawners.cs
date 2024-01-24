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

    
    private AudioSource audioSource;
    public AudioClip sound;

    
    private void Start()
    {
            boss = false;
            InvokeRepeating("LaunchProjectile", 5.0f, 3.0f);
            Debug.Log("Started!");
            
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
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
        audioSource.PlayOneShot(sound, 0.2f);
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
