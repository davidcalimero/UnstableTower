using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCreator : MonoBehaviour
{
    public GameObject[] prefabs;
    public Transform player;
    public Transform lastBlock;


    private float[] angles = {0,0f, 90.0f, 180.0f, 270.0f};
    public float playerMaxHeight = 0;
    public float nextStep = 10;
    public float step = 16;

    void Start()
    {
        SpawnBlock();
    }

    void Update()
    {
        float playerHeight = player.position.y;
        if(playerHeight > playerMaxHeight)
        {
            playerMaxHeight = playerHeight;

            if(playerMaxHeight > nextStep - 8)
            {
                SpawnBlock();
            }
        }
    }

    void SpawnBlock()
    {
        int blockIndex = Random.Range(0, prefabs.Length);
        int angleIndex = Random.Range(0, angles.Length);

        Instantiate(prefabs[blockIndex], new Vector3(-13, nextStep, 13), Quaternion.Euler(0.0f, angles[angleIndex], 0.0f));
        nextStep += step;
    }
}
