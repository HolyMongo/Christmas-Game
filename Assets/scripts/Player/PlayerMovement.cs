using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
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

    [Header("Ledge Grab")]
    [SerializeField] [Tooltip("True if the player can grab the ledge")] private bool isLedgeGrabable;
    [SerializeField] [Tooltip("True if the player is currently grabbing a ledge")] private bool isGrabbingLedge;
    [SerializeField] [Tooltip("Point from where a ray is cast to find a grabable ledge")] private Transform checkPos1;
    [SerializeField] [Tooltip("Point from where a ray is cast to find a grabable ledge")] private Transform checkPos2;
    [SerializeField] [Tooltip("Point from where a ray is cast to find a grabable ledge")] private float distanceBetween1and2;
    [SerializeField] [Tooltip("The layers that the player can grab")] private LayerMask ledgeGrabLayerMask;
    [SerializeField] [Tooltip("The minimus with of the ledge for the player to grab it")] private float ledgeWithGrab;
    [SerializeField] [Tooltip("The minimum with of the ledge for the player to pull itself up on the ledge")] private float ledgeWithStand;
    [SerializeField] [Tooltip("The distance it checks beneath the two points in order to fing a ledge to grab")] private float rayDownLenght;


    [Header("Test Things")]
    [SerializeField] [Tooltip("Testing purposes as of now")] private CharacterController cC;
    [SerializeField] [Tooltip("Testing purposes as of now")] private Transform cam;
    [SerializeField] [Tooltip("Testing purposes as of now")] private Vector3 relativeMoreDir;
    [SerializeField] [Tooltip("Testing purposes as of now")] private Vector3 moveDir;
    // Start is called before the first frame update
    void Start()
    {
        cC = gameObject.GetComponent<CharacterController>();
        cam = Camera.main.transform;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump_performed;
        distanceBetween1and2 = Vector3.Distance(checkPos1.position, checkPos2.position);
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        //If the character is grounded we set the y axis in the move direction to a positive value. aka set give the character an upward force
        if (isGrounded)
        {
            moveDir.y = Mathf.Sqrt(jumpPower * -2f * gravity);
            Debug.Log(obj.phase);
        }
        else
        {
            LedgeGrab();
        }
    }

    // Update is called once per frame
    void Update()
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

    private void LedgeGrab()
    {
        //cast down
        //cast forward
        //cast between
        RaycastHit cast1;
        RaycastHit cast2;
        RaycastHit cast3;

        Physics.Raycast(checkPos1.position, Vector3.down, out cast1, rayDownLenght, ledgeGrabLayerMask);
        Physics.Raycast(checkPos1.position, Vector3.down, out cast2, rayDownLenght, ledgeGrabLayerMask);

        if (cast1.transform != null && cast2.transform != null)
        {
            Debug.Log("Not Fail");
            Physics.Raycast(checkPos1.position, gameObject.transform.forward, out cast1, ledgeWithGrab, ledgeGrabLayerMask);
            Physics.Raycast(checkPos1.position, gameObject.transform.forward, out cast2, ledgeWithGrab, ledgeGrabLayerMask);

            if (cast1.transform == null && cast2.transform == null)
            {
                Debug.Log("Not Fail x2");

                Vector3 dirBetween = checkPos1.position - checkPos2.position;
                Physics.Raycast(checkPos2.position, dirBetween, out cast3, distanceBetween1and2, ledgeGrabLayerMask);
                if (cast3.transform == null)
                {
                    Debug.Log("Not Fail x3");
                    isLedgeGrabable = true;
                    isGrabbingLedge = true;
                }
                else
                {
                    Debug.Log("Fail x3");
                }
            }
            else
            {
                Debug.Log("Fail x2");
            }
        }
        else
        {
            Debug.Log("Fail");
        }
    }
}
