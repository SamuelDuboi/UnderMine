using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimeManager : MonoBehaviour
{

    public float maxTime = 300.0f;
    public float timeLeft;

    void Start()
    {
        ResetTime();    
    }

    void Update()
    {
        maxTime -= Time.deltaTime;
    }

    public void ResetTime()
    {
        timeLeft = maxTime;
    }
}
