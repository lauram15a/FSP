using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    //private Rigidbody rb;

    [Header("Camera")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float cameraVerticalAngle;

    [Header ("Move")]    
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private Vector3 moveInput;
    [SerializeField] private Vector3 rotationInput;
    [SerializeField] private float rotationSensibility;

    [Header("Jump")]
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravityScale;

    #region Awake(), Start() and Update

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        walkSpeed = 5;
        runSpeed = 10;
        jumpHeight = 1.9f;
        gravityScale = -20f;
        moveInput = Vector3.zero;
        rotationInput = Vector3.zero;
        rotationSensibility = 30;
    }

    // Update is called once per frame
    void Update()
    {
        Look();
        Move();
    }

    #endregion

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
    }

    private void Look()
    {
        rotationInput.x = Input.GetAxis("Mouse X") * rotationSensibility * Time.deltaTime;
        rotationInput.y = Input.GetAxis("Mouse Y") * rotationSensibility * Time.deltaTime;

        cameraVerticalAngle = cameraVerticalAngle + rotationInput.y;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -70, 70);

        transform.Rotate(Vector3.up * rotationInput.x);
        playerCamera.transform.localRotation = Quaternion.Euler(-cameraVerticalAngle, 0, 0);
    }
}
