using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMovement : MonoBehaviour, IMovementTypes
{
    PlayerInputActions playerInputActions;

    [Header("Movement Values")]
    [SerializeField] [Tooltip("Walking speed of the player")] private float speed;
    [SerializeField] [Tooltip("The multyplier to the walking speed when the player is sprinting")] private float sprintMulti;
    [SerializeField] [Tooltip("With how much force the player jumps")] private float jumpPower;
    [SerializeField] [Tooltip("The gravitational force exerted on the player")] private float gravity;

    [Header("Ground Check")]
    [SerializeField] [Tooltip("Is true when the player is grounded")] private bool isGrounded;
    [SerializeField] [Tooltip("The position of where the ground check takes place")] private Transform groundCheck;
    [SerializeField] [Tooltip("Radius of the sphere that checks for ground")] private float groundDistance;
    [SerializeField] [Tooltip("What layers the player sees as ground. Aka what can the player stand on, jump from, etc")] private LayerMask groundLayer;

    [Header("Test Things")]
    [SerializeField] [Tooltip("Testing purposes as of now")] private CharacterController cC;
    [SerializeField] [Tooltip("Testing purposes as of now")] private Transform cam;
    [SerializeField] [Tooltip("Testing purposes as of now")] private Vector3 relativeMoreDir;
    [SerializeField] [Tooltip("Testing purposes as of now")] private Vector3 moveDir;

    void Start()
    {
        cC = gameObject.GetComponent<CharacterController>();
        cam = Camera.main.transform;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump_performed;
    }

    public void Movement()
    {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>().normalized;

        Vector3 camRight = cam.right;
        Vector3 camForward = cam.forward;

        if (playerInputActions.Player.Sprint.IsPressed())
        {
            Debug.Log(inputVector);
            inputVector *= sprintMulti;
            Debug.Log("After" + inputVector);
        }

        camForward.y = 0;
        camRight.y = 0;

        camRight = camRight.normalized;
        camForward = camForward.normalized;

        Vector3 relativeForward = inputVector.y * camForward;
        Vector3 relativeRight = inputVector.x * camRight;

        relativeMoreDir = relativeForward + relativeRight;
        cC.Move(new Vector3(relativeMoreDir.x * speed, moveDir.y, relativeMoreDir.z * speed) * Time.deltaTime);
        Gravity();
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        //If the character is grounded we set the y axis in the move direction to a positive value. aka set give the character an upward force
        if (isGrounded)
        {
            moveDir.y = Mathf.Sqrt(jumpPower * -2f * gravity);
            Debug.Log(obj.phase);
        }
    }

    private void Gravity()
    {
        if (IsGrounded() && moveDir.y < 0)
        {
            moveDir.y = -2f;
        }

        moveDir.y += gravity * Time.deltaTime;
    }

    private bool IsGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);
        return isGrounded;
    }

    public void EnableOrDisableActionMap(bool EnableOrDisable)
    {
        if (EnableOrDisable)
        {
            playerInputActions.Player.Enable();
        }
        else
        {
            playerInputActions.Player.Disable();
        }
    }
}
