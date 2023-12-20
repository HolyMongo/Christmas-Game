using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICandyCane : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Destroy(this.gameObject);
    }
}
