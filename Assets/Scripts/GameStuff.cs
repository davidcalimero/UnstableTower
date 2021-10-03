using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStuff : MonoBehaviour
{
    public static float initialtime;
    public static float timeUntilStart = 10.0f;
    public static bool start = false;


    private float timePassed = 0;

    void Awake()
    {
        start = false;
        initialtime = Time.realtimeSinceStartup;
    }

    void Update()
    {
        timePassed += Time.deltaTime;
        if(timePassed > timeUntilStart)
        {
            
            start = true;
        }
    }
}