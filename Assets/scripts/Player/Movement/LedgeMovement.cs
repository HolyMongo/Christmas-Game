using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeMovement : MonoBehaviour, IMovementTypes
{
    PlayerInputActions playerInputActions;

    [Header("Movement Values")]
    [SerializeField] [Tooltip("Walking speed of the player")] private float speed;


    [Header("Ledge Grab")]
    [SerializeField] [Tooltip("True if the player can grab the ledge")] private bool isLedgeGrabable; //Currently not being used
    [SerializeField] [Tooltip("True if the player is currently grabbing a ledge")] private bool isGrabbingLedge;
    [SerializeField] [Tooltip("Point from where a ray is cast to find a grabable ledge")] private Transform checkPos1;
    [SerializeField] [Tooltip("Point from where a ray is cast to find a grabable ledge")] private Transform checkPos2;
    [SerializeField] [Tooltip("Distance Between the two points where we check for a grabable ledge")] private float distanceBetween1and2;
    [SerializeField] [Tooltip("The layers that the player can grab")] private LayerMask ledgeGrabLayerMask;
    [SerializeField] [Tooltip("The minimus with of the ledge for the player to grab it")] private float ledgeWithGrab;
    [SerializeField] [Tooltip("The minimum with of the ledge for the player to pull itself up on the ledge")] private float ledgeWithStand;
    [SerializeField] [Tooltip("The distance it checks beneath the two points in order to fing a ledge to grab")] private float rayDownLenght;
    [SerializeField] [Tooltip("The distance the player should be betheathe the ledge. Aka the lenght of the arms")] private float heightOfset;

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
        //playerInputActions.LedgeGrab.Enable();
        distanceBetween1and2 = Vector3.Distance(checkPos1.position, checkPos2.position);
    }

    public void Movement()
    {

    }

    public void EnableOrDisableActionMap(bool EnableOrDisable)
    {
        if (EnableOrDisable)
        {
            playerInputActions.LedgeGrab.Enable();
        }
        else
        {
            playerInputActions.LedgeGrab.Disable();
        }
    }
}
