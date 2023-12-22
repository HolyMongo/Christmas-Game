using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemySO/New Enemy")]
public class EnemySO : ScriptableObject
{
    [Header("Nav Mesh")]
    [SerializeField] private float agentHeight;
    [SerializeField] private float agentRadius;
    [SerializeField] private float agentStepHeight;
    [SerializeField] private float agentSlopeLimit;

    [Header("Detection")]
    [SerializeField] private float detectionRadius;
    [SerializeField] private float detectionDegres;

    [Header("Other")]
    [SerializeField] private float hp;
    [SerializeField] private float damage;
    [SerializeField] private float walkSpeed;


    public float GetAgentHeight()
    {
        return agentHeight;
    }
    public float GetAgentRadius()
    {
        return agentRadius;
    }
    public float GetAgentStepHeight()
    {
        return agentStepHeight;
    }
    public float GetAgentSlopeLimit()
    {
        return agentSlopeLimit;
    }
    public float GetDetectionRadius()
    {
        return detectionRadius;
    }
    public float GetDetectionDegres()
    {
        return detectionDegres;
    }
    public float GetHp()
    {
        return hp;
    }
    public float GetDamage()
    {
        return damage;
    }
    public float GetWalkspeed()
    {
        return walkSpeed;
    }

}
