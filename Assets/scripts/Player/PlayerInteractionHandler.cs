using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionHandler : MonoBehaviour
{
    PlayerInputActions playerInputActions;
    [SerializeField] private List<IInteractable> interactableObjects = new List<IInteractable>();

    [SerializeField] Vector3 halfExtents;
    [SerializeField] float interactionDistance;
    [SerializeField] RaycastHit[] rayhitList;
    [SerializeField] LayerMask interactionLayer;
    [SerializeField] GameObject closestInteractable;
    [SerializeField] IInteractable closestIInteractable;


    // Start is called before the first frame update
    void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.interact.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        float closestDistance = 999;
        Physics.BoxCast(transform.position, halfExtents, transform.forward, out RaycastHit ray, Quaternion.identity, interactionDistance);
        Physics.BoxCastAll(transform.position, halfExtents, transform.forward, Quaternion.identity, interactionDistance, interactionLayer);
        rayhitList = Physics.BoxCastAll(transform.position, halfExtents, transform.forward, Quaternion.identity, interactionDistance, interactionLayer);

        for (int i = 0; i < rayhitList.Length; i++)
        {
            if (Vector3.Distance(transform.position, rayhitList[i].transform.position) < closestDistance)
            {
                closestDistance = Vector3.Distance(transform.position, rayhitList[i].transform.position);
                closestInteractable = rayhitList[i].transform.gameObject;
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
}
