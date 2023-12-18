using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController cC;

    [Header("Movement Values")]
    [SerializeField] private float speed;
    [SerializeField] private float sprintMulti;
    [SerializeField] private float jumpPower;
    [SerializeField] private float gravity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
