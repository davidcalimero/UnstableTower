using UnityEngine;

public class AnimSprite : MonoBehaviour
{
    private bool isRunning;
    private bool isJumping;
    private bool isFalling;

    private Vector3 previousPosition;
    public ParticleSystem particles;
    public GameObject ground;
    public PlayerMovement player;

    public SoundManager jump;
    public SoundManager run;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Vector3 direction = (transform.position - previousPosition).normalized;
        float move = player.horizontalMove;

        if ((move < 0) && !isJumping && !isFalling && player.m_Grounded)
        {
            if (isFalling)
            {
                isFalling = false;
                animator.SetBool("Falling", false);
            }

            spriteRenderer.flipX = true;
            ground.transform.localEulerAngles = new Vector3(0, 180, 0);
            if (!isRunning)
            {
                isRunning = true;
                animator.SetBool("Running", true);
                particles.Play();
                run.PlaySound();
            }
        }
        else
        if ((move > 0) && !isJumping && !isFalling && player.m_Grounded)
        {
            if (isFalling)
            {
                isFalling = false;
                animator.SetBool("Falling", false);
            }

            spriteRenderer.flipX = false;
            ground.transform.localEulerAngles = new Vector3(0, 0, 0);

            if (!isRunning)
            {
                isRunning = true;
                animator.SetBool("Running", true);
                particles.Play();
                run.PlaySound();
            }
        }
        else

        if ((move < 0) && (isJumping || isFalling))
        {
            spriteRenderer.flipX = true;
            ground.transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        else
        if ((move> 0) && (isJumping || isFalling))
        {
            spriteRenderer.flipX = false;
            ground.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else if(direction.y > 0 && !player.m_Grounded)
        {
            isRunning = false;
            isJumping = true;
            animator.SetBool("Jumping", true);
            particles.Stop();
            run.StopSound();

        }
        else
        if (move == 0)
        {
            isRunning = false;
            animator.SetBool("Running", false);
            particles.Stop();
            run.StopSound();

        }       

        else
        if (!isFalling && direction.y < 0 && !player.m_Grounded)
        {
            isJumping = false;
            isFalling = true;
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", true);
            run.StopSound();
        }
        if (player.m_Grounded || transform.position == previousPosition)
        {
            isJumping = false;
            isFalling = false;
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", false);
        }

        previousPosition = transform.position;
    }
}
