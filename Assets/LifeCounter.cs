using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCounter : MonoBehaviour
{

    public GameObject Life1;
    public GameObject Life2;
    public GameObject Life3;
    
    // Start is called before the first frame update

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
    }
}
