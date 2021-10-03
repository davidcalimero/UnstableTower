using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBackroundCubes : MonoBehaviour
{
    Transform player;

    Vector3 pos;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        pos = transform.position;
        StartCoroutine(AnimObject());
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.position) > 100)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator AnimObject()
    {

        float space = Random.Range(0, 5);

        float timer = Random.Range(0, 5);


        float elapsed = 0;
        while(elapsed < 1)
        {
            elapsed += Time.deltaTime / timer;

            transform.localPosition = Vector3.Lerp(pos, pos + (Vector3.up * space), elapsed);

            yield return null;
        }

        elapsed = 0;
        timer = Random.Range(0, 5);

        while (elapsed < 1)
        {
            elapsed += Time.deltaTime / timer;

            transform.localPosition = Vector3.Lerp(pos + (Vector3.up * space), pos, elapsed);

            yield return null;
        }

        StartCoroutine(AnimObject());

    }
}
