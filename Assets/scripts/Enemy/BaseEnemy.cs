using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemySO enemySO;

    [Header("Enemy stats")]
    [SerializeField] private float hp;
    [SerializeField] private float damage;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    [Header("AI variables")]
    [SerializeField] private float agentHeight;
    [SerializeField] private float agentRadius;
    [SerializeField] private float agentStepHeight;
    [SerializeField] private float agentSlopeLimit;

    [Header("AI detection")]
    [SerializeField] private float detectionRadius;
    [SerializeField] private float detectionDegres;


    // Start is called before the first frame update
    void Start()
    {
        //Enemy stats
        hp = enemySO.GetHp();
        damage = enemySO.GetDamage();
        walkSpeed = enemySO.GetWalkspeed();
        runSpeed = enemySO.GetRunSpeed();

        //AI variables
        agentHeight = enemySO.GetAgentHeight();
        agentRadius = enemySO.GetAgentRadius();
        agentStepHeight = enemySO.GetAgentStepHeight();
        agentSlopeLimit = enemySO.GetAgentSlopeLimit();

        //AI detection
        detectionRadius = enemySO.GetDetectionRadius();
        detectionDegres = enemySO.GetDetectionDegres();
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
