using UnityEngine;

public class PiecesSpawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public Transform target;

    public Transform player;

    public int number_of_objects = 10;
    public float min_object_distance = 20.0f;
    public float min_target_distsnce = 30.0f;
    public float max_target_distance = 60.0f;

    private float lastPlayerHeight = 0;

    private Vector3[] positions;


    void Start()
    {
        SpawnObjects(new Vector2(player.position.y - 30, player.position.y + 30));
        lastPlayerHeight = Mathf.Round(player.position.y);
    }

    void Update()
    {
        float newHeight = Mathf.Round(player.position.y);
        if(lastPlayerHeight < newHeight)
        {
            SpawnObjects(new Vector2(lastPlayerHeight + 31, newHeight + 30));
            lastPlayerHeight = newHeight;
        }
    }

    void SpawnObjects(Vector2 range)
    {
        if(prefabs.Length < 1)
        {
            return;
        }

        for (float height = range.x; height <= range.y; ++height)
        {
            positions = new Vector3[number_of_objects];

            for (int count = 0; count < number_of_objects; ++count)
            {
                int index = Random.Range(0, prefabs.Length);
                GameObject prefab = prefabs[index];
                Vector3 position = GetRandomPosition(height);
                positions[count] = position;

                GameObject instance = Instantiate(prefab, position, Quaternion.Euler(Random.Range(0, 359), Random.Range(0, 359), Random.Range(0, 359)));
                instance.AddComponent<DestroyBackroundCubes>();
                
            }
        }
    }

    Vector3 GetRandomPosition(float y)
    {
        Vector2 randomPoint = Random.insideUnitCircle.normalized;
        Vector3 result = new Vector3(randomPoint.x, 0, randomPoint.y);
        result *= Random.Range(min_target_distsnce, max_target_distance);
        result += transform.position;
        result.x += -13;
        result.z += 13;
        result.y = y;

        foreach (Vector3 vector in positions)
        {
            if(Mathf.Abs(Vector3.Distance(vector, result)) < min_object_distance)
            {
                return GetRandomPosition(y);
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
