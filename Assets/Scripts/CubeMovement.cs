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
    public Vector3 destination;
    private float lastTick = 0;
    private float interval = 0;
    private MeshRenderer meshRenderer;

    private Vector2 lifeTime = new Vector2(0.0f, 1.0f);
    private float height;
    private float currentLifeTime;
    public bool died = false;

    private float timeChanged;

    void Start()
    {
        destination = originalPosition = transform.position;
        interval = Random.Range(moveInterval.x, moveInterval.y);
        
        height = transform.position.y;
        currentLifeTime = Random.Range(lifeTime.x, lifeTime.y) + (0.5f * transform.position.y);

        meshRenderer = GetComponent<MeshRenderer>();
        timeChanged = Time.time;
    }

    void Update()
    {
        if(GameStuff.start)
        {
            CheckDeath();
        }
        
        if(!died)
        {
            Move(false);
            UpdateVisual();
        }
    }

    void UpdateVisual()
    {
        if(!meshRenderer.isVisible)
        {
            return;
        }

        if(Time.time - timeChanged > .3f)
        {
            timeChanged = Time.time;
            float value1 = (Random.value > 0.5 ? 1 : -1) * transform.localScale.x;
            float value2 = (Random.value > 0.5 ? 1 : -1) * transform.localScale.y;
            float value3 = (Random.value > 0.5 ? 1 : -1) * transform.localScale.z;
            transform.localScale = new Vector3(value1, value2, value3);
        }
    }

    void CheckDeath()
    {
        if(transform.position.y <= originalPosition.y - 29)
        {
            Destroy(gameObject);
            return;
        }

        
        if(Time.realtimeSinceStartup - GameStuff.initialtime > currentLifeTime)
        {
            died = true;
            destination = new Vector3(transform.position.x, transform.position.y - 30, transform.position.z);
        }
    }

    void Move(bool force)
    {
        if(!allowMoveBack && !allowMoveForward && !allowMoveLeft && !allowMoveRight && !allowMoveUp && !allowMoveDown)
        {
            return;
        }
        
        lastTick += Time.deltaTime;
        if(!force && lastTick < interval)
        {
            return;
        }

        lastTick = 0;
        interval = Random.Range(moveInterval.x, moveInterval.y);

        if(!meshRenderer.isVisible)
        {
            transform.position = destination;
            return;
        }

        int count = 0;

        List<Vector3> possibleMoves = new List<Vector3>();
        if(originalPosition == transform.position)
        {
            if(allowMoveUp && !CheckColision(transform.up))
            {
                possibleMoves.Add(transform.up);
                ++count;
            }
            if(allowMoveDown && !CheckColision(-transform.up))
            {
                possibleMoves.Add(-transform.up);
                ++count;
            }
            if(allowMoveLeft && !CheckColision(-transform.right))
            {
                possibleMoves.Add(-transform.right);
                ++count;
            }
            if(allowMoveRight && !CheckColision(transform.right))
            {
                possibleMoves.Add(transform.right);
                ++count;
            }
            if(allowMoveForward && !CheckColision(transform.forward))
            {
                possibleMoves.Add(transform.forward);
                ++count;
            }
            if(allowMoveBack && !CheckColision(-transform.forward))
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
        destination = transform.position + (direction * movement);
    }

    void FixedUpdate()
    {
        if(transform.position != destination)
        {
            float x = Mathf.Lerp(transform.position.x, destination.x, 0.07f);
            float y = Mathf.Lerp(transform.position.y, destination.y, 0.07f);
            float z = Mathf.Lerp(transform.position.z, destination.z, 0.07f);
            transform.position = new Vector3(x, y, z);
        }
    }

    bool CheckColision(Vector3 direction)
    {
        return Physics.Raycast(transform.position, direction, 1.0f, LayerMask.GetMask("Map"));
    }
}
