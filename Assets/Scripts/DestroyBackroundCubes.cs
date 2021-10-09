using UnityEngine;

public class DestroyBackroundCubes : MonoBehaviour
{
    Transform player;

    Vector3 initialPos;
    Vector3 destination;
    float elapsed = 0;
    float timer;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        initialPos = transform.position;
        destination = initialPos + (Vector3.up * Random.Range(0, 5) * (Random.value >= 0.5f ? 1 : -1));
        timer = Random.Range(1, 5);
    }

    void FixedUpdate()
    {
        if((player.position.y - transform.position.y) > 35)
        {
            Destroy(gameObject);
            return;
        }
        else if(transform.position == destination)
        {
               var temp = initialPos;
               initialPos = destination;
               destination = temp;
               elapsed = 0;
        }

        elapsed += Time.deltaTime / timer;
        transform.localPosition = Vector3.Lerp(initialPos, destination, elapsed);
    }
}
