using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	public bool m_Grounded;            // Whether or not the player is grounded.
	private Rigidbody m_Rigidbody;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

    public float horizontalMove;
    private bool jump = false;
	private bool doubleJump = false;
	private int doubleJumpCount = 0;
	public int maxJumps = 1;
    public float runSpeed = 40;

	public SoundManager jumpSound;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	private void Awake()
	{
		m_Rigidbody = GetComponent<Rigidbody>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
	}

	private void FixedUpdate()
	{
        Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;

		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider[] colliders = Physics.OverlapSphere(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				doubleJumpCount = 0;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}


	public void Move(float move, bool shouldJump)
	{
        //only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			// Move the character by finding the target velocity
            Vector3 targetVelocity = transform.forward * move * 10f;
            targetVelocity.y = m_Rigidbody.velocity.y;
			// And then smoothing it out and applying it to the character
			m_Rigidbody.velocity = Vector3.SmoothDamp(m_Rigidbody.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
		}
		// If the player should jump...
		if (m_Grounded && shouldJump)
		{
			jumpSound.PlaySound();

			// Add a vertical force to the player.
			m_Grounded = false;
			m_Rigidbody.AddForce(new Vector3(0f, m_JumpForce, 0f));
		}

		if (!m_Grounded && doubleJump)
		{
			jumpSound.PlaySound();

			doubleJump = false;
			// Add a vertical force to the player.
			m_Grounded = false;
			m_Rigidbody.velocity = Vector3.zero;
			m_Rigidbody.AddForce(new Vector3(0f, m_JumpForce/1.25f, 0f));
		}
	}

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

		if (Input.GetButtonDown("Jump") && !m_Grounded && !doubleJump && doubleJumpCount < maxJumps)
		{
			doubleJumpCount++;
			doubleJump = true;
		}
	}
}
