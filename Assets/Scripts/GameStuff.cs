using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStuff : MonoBehaviour
{
    public static float initialtime;

    void Awake()
    {
        initialtime = Time.realtimeSinceStartup;
    }
}