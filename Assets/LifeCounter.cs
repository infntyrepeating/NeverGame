using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCounter : MonoBehaviour
{
	public static bool Passive = false;
    private float timer = 0;
    
    public GameObject Life1;
    public GameObject Life2;
    public GameObject Life3;

    [SerializeField] private float timer_cooldown = 3f;
    
    public static float timeStamp;
    
    // Start is called before the first frame update

    private void Start()
    {
        timer = timer_cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMovement.life == 3)
        {
            Life1.SetActive(true);
            Life2.SetActive(true);
            Life3.SetActive(true);
        } else if (PlayerMovement.life == 2)
        {
            Life1.SetActive(true);
            Life2.SetActive(true);
            Life3.SetActive(false);
        } else if (PlayerMovement.life == 1)
        {
            Life1.SetActive(true);
            Life2.SetActive(false);
            Life3.SetActive(false);
        }

        if (Passive)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                timer = timer_cooldown;
                Passive = false;
            }
        }
    }

	public static void Lose(){
		if(!Passive)
        {
            Debug.Log("começou!");
            Passive = true;
            timeStamp = Time.time + 3f;
            PlayerMovement.life--;
        }
	}
}
