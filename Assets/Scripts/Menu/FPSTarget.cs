using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSTarget : MonoBehaviour
{
    void Awake()
    {
        Application.targetFrameRate = 60;
    }
}
