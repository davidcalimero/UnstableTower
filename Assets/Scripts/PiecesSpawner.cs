using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecesSpawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public Transform target;

    public int number_of_objects = 100;
    public float min_object_distance = 3.0f;
    public float min_target_distsnce = 10.0f;
    public float max_target_distance = 20.0f;

    private Vector3[] positions;


    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        positions = new Vector3[number_of_objects];

        if(prefabs.Length < 1)
        {
            return;
        }

        for (int count = 0; count < number_of_objects; ++count)
        {
            int index = Random.Range(0, prefabs.Length);
            GameObject prefab = prefabs[index];
            Vector3 position = GetRandomPosition();
            positions[count] = position;

            Instantiate(prefab, position, Quaternion.Euler(0, 0, 0));

        }
    }

    Vector3 GetRandomPosition()
    {
        Vector3 result = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        result *= Random.Range(min_target_distsnce, max_target_distance);
        result += transform.position;

        foreach (Vector3 vector in positions)
        {
            if(Mathf.Abs(Vector3.Distance(vector, result)) < min_object_distance)
            {
                return GetRandomPosition();
            }
        }

        return result;
    }

    float GetRandomNumber(float min, float max)
    {
        float signal =  Random.value > 0.5f ? 1.0f : -1.0f;
        float number =  Random.Range(min, max);
        return number * signal;
    }
}
