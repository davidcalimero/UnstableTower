using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBackroundCubes : MonoBehaviour
{
    Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.position) > 100)
        {
            Destroy(gameObject);
        }
    }
}
