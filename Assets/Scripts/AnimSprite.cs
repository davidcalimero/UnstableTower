using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSprite : MonoBehaviour
{
    private bool isRunning;
    private bool isJumping;
    private bool isFalling;

    private Vector3 previousPosition;

    private void Update()
    {
        if (Input.GetKey(KeyCode.A) && !isJumping && !isFalling && transform.parent.GetComponent<PlayerMovement>().m_Grounded)
        {
            if (isFalling)
            {
                isFalling = false;
                GetComponent<Animator>().SetBool("Falling", false);
            }

            GetComponent<SpriteRenderer>().flipX = true;
            if (!isRunning)
            {
                isRunning = true;
                GetComponent<Animator>().SetBool("Running", true);
            }
        }
        if (Input.GetKey(KeyCode.D) && !isJumping && !isFalling && transform.parent.GetComponent<PlayerMovement>().m_Grounded)
        {
            if (isFalling)
            {
                isFalling = false;
                GetComponent<Animator>().SetBool("Falling", false);
            }

            GetComponent<SpriteRenderer>().flipX = false;
            if (!isRunning)
            {
                isRunning = true;
                GetComponent<Animator>().SetBool("Running", true);
            }
        }

        if (Input.GetKey(KeyCode.A) && (isJumping || isFalling))
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Input.GetKey(KeyCode.D) && (isJumping || isFalling))
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if ((Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)))
        {
            isRunning = false;
            GetComponent<Animator>().SetBool("Running", false);
        }
        Vector3 direction = (transform.position - previousPosition).normalized;

        if (direction.y > 0 && !transform.parent.GetComponent<PlayerMovement>().m_Grounded)
        {
            isRunning = false;
            isJumping = true;
            GetComponent<Animator>().SetBool("Jumping", true);
        }


        if (!isFalling && direction.y < 0 && !transform.parent.GetComponent<PlayerMovement>().m_Grounded)
        {
            print("JER");
            isJumping = false;
            isFalling = true;
            GetComponent<Animator>().SetBool("Jumping", false);
            GetComponent<Animator>().SetBool("Falling", true);
        }

        if (isFalling && transform.parent.GetComponent<PlayerMovement>().m_Grounded)
        {
            isFalling = false;
            GetComponent<Animator>().SetBool("Falling", false);
        }

        previousPosition = transform.position;
    }
}
