using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeResetter : MonoBehaviour
{ 
    void Awake()
    {
        Time.timeScale = 1f;
    }
}
