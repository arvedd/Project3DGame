using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    public float speed = 5f;
    public float jumpForce = 5f;
    public float gravityValue = -9.81f;
    public float sensivity = 0.5f;
    public float xRotation = 0f;
    public Transform groundCheck; // empty object di bawah kaki player
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool groundedPlayer;

    public Transform cameraTransform;
    public InputActionReference moveAction;
    public InputActionReference jumpAction;
    public InputActionReference lookAction;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = GetComponentInChildren<Camera>().transform;
    }

    private void OnEnable()
    {
        moveAction.action.Enable();
        lookAction.action.Enable();
        jumpAction.action.Enable();
        jumpAction.action.performed += OnJump;
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        lookAction.action.Disable();
        jumpAction.action.Disable();
        jumpAction.action.performed -= OnJump;
    }

    void Update()
    {
        groundedPlayer = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        // Movement
        Vector2 moveInput = moveAction.action.ReadValue<Vector2>();
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * speed * Time.deltaTime);

        // Look / rotation
        Vector2 lookInput = lookAction.action.ReadValue<Vector2>();
        transform.Rotate(Vector3.up * lookInput.x * sensivity);

        xRotation -= lookInput.y * sensivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Gravity
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }


    public void OnJump(InputAction.CallbackContext context)
    {
        if (groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpForce * -2f * gravityValue);
        }

       
    }
}
