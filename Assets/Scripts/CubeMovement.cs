using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public Vector2 moveInterval = new Vector2(5.0f, 10.0f);
    public float movement = 2.0f;

    public bool allowMoveUp = false;
    public bool allowMoveDown = false;
    public bool allowMoveRight = false;
    public bool allowMoveLeft = false;
    public bool allowMoveForward = false;
    public bool allowMoveBack = false;

    public Vector3 originalPosition;

    private float lastTick = 0;
    private float interval = 0;

    void Start()
    {
        originalPosition = transform.position;
        interval = Random.Range(moveInterval.x, moveInterval.y);
        Move(true);
    }

    void Update()
    {
        Move(false);
    }

    void Move(bool force)
    {
        lastTick += Time.deltaTime;
        if(!force && lastTick < interval)
        {
            return;
        }

        lastTick = 0;
        interval = Random.Range(moveInterval.x, moveInterval.y);

        int count = 0;

        List<Vector3> possibleMoves = new List<Vector3>();
        if(originalPosition == transform.position)
        {
            if(!CheckColision(transform.up) && allowMoveUp)
            {
                possibleMoves.Add(transform.up);
                ++count;
            }
            if(!CheckColision(-transform.up) && allowMoveDown)
            {
                possibleMoves.Add(-transform.up);
                ++count;
            }
            if(!CheckColision(-transform.right) && allowMoveLeft)
            {
                possibleMoves.Add(-transform.right);
                ++count;
            }
            if(!CheckColision(transform.right) && allowMoveRight)
            {
                possibleMoves.Add(transform.right);
                ++count;
            }
            if(!CheckColision(transform.forward) && allowMoveForward)
            {
                possibleMoves.Add(transform.forward);
                ++count;
            }
            if(!CheckColision(-transform.forward) && allowMoveBack)
            {
                possibleMoves.Add(-transform.forward);
                ++count;
            }
        }
        else
        {
            possibleMoves.Add((originalPosition - transform.position).normalized);
            count++;
        }

        if(count == 0)
        {
            return;
        }

        int index = Random.Range(0, count);
        Vector3 direction = possibleMoves[index];
        StartCoroutine(Translate(transform.position + (direction * movement)));
    }

    IEnumerator Translate(Vector3 destination)
    {
        float elapsedTime = 0;
        while(elapsedTime < 1)
        {
            elapsedTime += Time.deltaTime;
            float x = Mathf.Lerp(transform.position.x, destination.x, elapsedTime);
            float y = Mathf.Lerp(transform.position.y, destination.y, elapsedTime);
            float z = Mathf.Lerp(transform.position.z, destination.z, elapsedTime);
            transform.position = new Vector3(x, y, z);
            yield return null;
        }
    }

    bool CheckColision(Vector3 direction)
    {
        return Physics.Raycast(transform.position, direction, 1.0f, LayerMask.GetMask("Map"));
    }
}
