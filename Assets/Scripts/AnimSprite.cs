using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSprite : MonoBehaviour
{
    private bool isRunning;
    private bool isJumping;
    private bool isFalling;

    private Vector3 previousPosition;
    public ParticleSystem particles;
    public GameObject ground;

    public SoundManager jump;
    public SoundManager run;

    private void Update()
    {
        Vector3 direction = (transform.position - previousPosition).normalized;

        if ((Input.GetAxisRaw("Horizontal") < 0) && !isJumping && !isFalling && transform.parent.GetComponent<PlayerMovement>().m_Grounded)
        {
            if (isFalling)
            {
                isFalling = false;
                GetComponent<Animator>().SetBool("Falling", false);
            }

            GetComponent<SpriteRenderer>().flipX = true;
            ground.transform.localEulerAngles = new Vector3(0, 180, 0);
            if (!isRunning)
            {
                isRunning = true;
                GetComponent<Animator>().SetBool("Running", true);
                particles.Play();
                run.PlaySound();
            }
        }
        else
        if ((Input.GetAxisRaw("Horizontal") > 0) && !isJumping && !isFalling && transform.parent.GetComponent<PlayerMovement>().m_Grounded)
        {
            if (isFalling)
            {
                isFalling = false;
                GetComponent<Animator>().SetBool("Falling", false);
            }

            GetComponent<SpriteRenderer>().flipX = false;
            ground.transform.localEulerAngles = new Vector3(0, 0, 0);

            if (!isRunning)
            {
                isRunning = true;
                GetComponent<Animator>().SetBool("Running", true);
                particles.Play();
                run.PlaySound();
            }
        }
        else

        if ((Input.GetAxisRaw("Horizontal") < 0) && (isJumping || isFalling))
        {
            GetComponent<SpriteRenderer>().flipX = true;
            ground.transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        else
        if ((Input.GetAxisRaw("Horizontal") > 0) && (isJumping || isFalling))
        {
            GetComponent<SpriteRenderer>().flipX = false;
            ground.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else if(direction.y > 0 && !transform.parent.GetComponent<PlayerMovement>().m_Grounded)
        {
            isRunning = false;
            isJumping = true;
            GetComponent<Animator>().SetBool("Jumping", true);
            particles.Stop();
        }else
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            isRunning = false;
            GetComponent<Animator>().SetBool("Running", false);
            particles.Stop();
            run.StopSound();

        }
        
        

        else
        if (!isFalling && direction.y < 0 && !transform.parent.GetComponent<PlayerMovement>().m_Grounded)
        {
            isJumping = false;
            isFalling = true;
            GetComponent<Animator>().SetBool("Jumping", false);
            GetComponent<Animator>().SetBool("Falling", true);
            run.StopSound();
        }
        if (transform.parent.GetComponent<PlayerMovement>().m_Grounded || transform.position == previousPosition)
        {
            isJumping = false;
            isFalling = false;
            GetComponent<Animator>().SetBool("Jumping", false);
            GetComponent<Animator>().SetBool("Falling", false);
        }



        previousPosition = transform.position;
    }
}
