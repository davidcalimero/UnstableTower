using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSprite : MonoBehaviour
{
    private bool isRunning;
    private bool isJumping;
    private bool isFalling;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
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
        if (Input.GetKeyDown(KeyCode.D))
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
        if ((Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) && isRunning)
        {
            isRunning = false;
            GetComponent<Animator>().SetBool("Running", false);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            isJumping = true;
            GetComponent<Animator>().SetBool("Jumping", true);
        }

        if (Input.GetKeyUp(KeyCode.W) && isJumping)
        {
            isJumping = false;
            isFalling = true;
            GetComponent<Animator>().SetBool("Jumping", false);
            GetComponent<Animator>().SetBool("Falling", true);
        }

        if (isFalling && Input.GetKeyDown(KeyCode.S))
        {
            isFalling = false;
            GetComponent<Animator>().SetBool("Falling", false);
        }

    }
}
