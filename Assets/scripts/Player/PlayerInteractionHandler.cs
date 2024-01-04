using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionHandler : MonoBehaviour
{
    PlayerInputActions playerInputActions;

    //[SerializeField] Vector3 halfExtents;
    //[SerializeField] float interactionDistance;
    //[SerializeField] private Vector3 halfExtent;


    [Header("Sphere variables")]
    [SerializeField] private Vector3 center;
    [SerializeField] private float radius;
    [SerializeField] private float maxDistance;
    [SerializeField] LayerMask interactionLayer;

    [Header("Output")]
    [SerializeField] GameObject closestInteractable;
    [SerializeField] RaycastHit[] rayhitList = new RaycastHit[10];
    [SerializeField] IInteractable closestIInteractable;


    [SerializeField] Vector3 gizmoDraw;


    // Start is called before the first frame update
    void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.interact.Enable();
        playerInputActions.Player.interact.performed += Interact_performed;

        center = transform.position + transform.forward * radius;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (closestIInteractable != null)
        {
            closestIInteractable.Interact();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float closestDistance = 999;
        //rayhitList = Physics.BoxCastAll(transform.position, halfExtents, transform.forward, Quaternion.identity, interactionDistance, interactionLayer);
        System.Array.Clear(rayhitList, 0, rayhitList.Length);
        center = transform.position + transform.forward * radius;
        Physics.SphereCastNonAlloc(center, radius, transform.forward, rayhitList, maxDistance, interactionLayer);

        if (rayhitList[0].transform != null)
        {
            for (int i = 0; i < rayhitList.Length; i++)
            {
                if (rayhitList[i].transform != null)
                {

                    Debug.Log(i);
                    Debug.Log(rayhitList[i].transform);
                    if (Vector3.Distance(transform.position, rayhitList[i].transform.position) < closestDistance)
                    {
                        closestDistance = Vector3.Distance(transform.position, rayhitList[i].transform.position);
                        closestInteractable = rayhitList[i].transform.gameObject;
                    }
                }
            }
            if (rayhitList.Length > 0)
            {
                closestInteractable.TryGetComponent<IInteractable>(out closestIInteractable);
            }
            else
            {
                closestInteractable = null;
            }
        }
        else
        {
            closestInteractable = null;
            closestIInteractable = null;
        }
    }
    private void OnDrawGizmos()
    {
        
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(center, radius);
    }
}
