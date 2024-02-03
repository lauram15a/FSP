using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private Rigidbody rb;
    
    private float walkSpeed;
    private float runSpeed;
    private float jumpHeight;
    //private float jumpForce;
    private float gravityScale;
    private Vector3 moveInput;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        walkSpeed = 5;
        runSpeed = 10;
        jumpHeight = 1.9f;
        //jumpForce = 6f;
        gravityScale = -20f;
        moveInput = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (characterController.isGrounded)
        {
            moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            moveInput = Vector3.ClampMagnitude(moveInput, 1f);

            if (Input.GetButton("Sprint"))
            {
                moveInput = transform.TransformDirection(moveInput) * runSpeed;
            }
            else
            {
                moveInput = transform.TransformDirection(moveInput) * walkSpeed;
            }

            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }

        moveInput.y += gravityScale * Time.deltaTime;
        characterController.Move(moveInput * Time.deltaTime);
    }

    private void Jump()
    {
        moveInput.y = Mathf.Sqrt(jumpHeight * -2f * gravityScale);
        //rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        //rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
