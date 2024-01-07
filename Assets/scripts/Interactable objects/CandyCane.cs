using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyCane : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Destroy(gameObject);
    }
}
