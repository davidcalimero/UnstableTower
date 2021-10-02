using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        moveDirection = transform.forward * Input.GetAxis("Horizontal");
        moveDirection *= speed;

        if (Input.GetButton("Jump"))
        {
            moveDirection.y = jumpSpeed;
        }

        if (Input.GetKeyDown("w"))
        {
            transform.RotateAround(transform.position, transform.up, -90f);
        }

        if (Input.GetKeyDown("s"))
        {
            transform.RotateAround(transform.position, transform.up, 90f);
        }

        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
