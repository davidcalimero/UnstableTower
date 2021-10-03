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

    public bool collidingUp = false;
    public bool collidingDown = false;
    public bool collidingLeft = false;
    public bool collidingRight = false;
    public bool collidingForward = false;
    public bool collidingBack = false;

    public Vector3 originalPosition;

    private float lastTick;
    private float interval;

    void Start()
    {
        collidingUp = CheckColision(Vector3.up);
        collidingDown = CheckColision(Vector3.down);
        collidingLeft = CheckColision(Vector3.left);
        collidingRight = CheckColision(Vector3.right);
        collidingForward = CheckColision(Vector3.forward);
        collidingBack = CheckColision(Vector3.back);

        originalPosition = transform.position;
        interval = Random.Range(moveInterval.x, moveInterval.y);
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        lastTick += Time.deltaTime;
        if(lastTick < interval)
        {
            return;
        }

        lastTick = 0;
        interval = Random.Range(moveInterval.x, moveInterval.y);

        int count = 0;

        List<Vector3> possibleMoves = new List<Vector3>();
        if(originalPosition == transform.position)
        {
            if(!collidingUp && allowMoveUp)
            {
                possibleMoves.Add(Vector3.up);
                ++count;
            }
            if(!collidingDown && allowMoveDown)
            {
                possibleMoves.Add(Vector3.down);
                ++count;
            }
            if(!collidingLeft && allowMoveLeft)
            {
                possibleMoves.Add(Vector3.left);
                ++count;
            }
            if(!collidingRight && allowMoveRight)
            {
                possibleMoves.Add(Vector3.right);
                ++count;
            }
            if(!collidingForward && allowMoveForward)
            {
                possibleMoves.Add(Vector3.forward);
                ++count;
            }
            if(!collidingBack && allowMoveBack)
            {
                possibleMoves.Add(Vector3.back);
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

    IEnumerator Translate(Vector3 destiny)
    {
        float elapsedTime = 0;
        while(elapsedTime < 1)
        {
            elapsedTime += Time.deltaTime;
            float x = Mathf.Lerp(transform.position.x, destiny.x, elapsedTime);
            float y = Mathf.Lerp(transform.position.y, destiny.y, elapsedTime);
            float z = Mathf.Lerp(transform.position.z, destiny.z, elapsedTime);
            transform.position = new Vector3(x, y, z);
            yield return null;
        }
    }

    bool CheckColision(Vector3 direction)
    {
        return Physics.Raycast(transform.position, direction, 1.5f, LayerMask.GetMask("Map"));
    }
}
