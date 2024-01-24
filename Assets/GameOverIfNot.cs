using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverIfNot : MonoBehaviour
{
    public bool timeEnded = false;

    private void Update()
    {
        if (timeEnded)
        {
            SceneManager.LoadScene(5);
        }
    }
}
